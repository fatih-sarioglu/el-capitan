using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject startCanvas;
    [SerializeField]
    private GameObject gameplayCanvas;
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject canvas;

    private SwipeAndTapManager swipeAndTapManager;

    private CameraShakeScript cameraShakeScript;

    [HideInInspector]
    public StaminaController staminaController;

    private CameraController cameraController;

    public static RockSpawner rockSpawner;

    private Rigidbody rb;

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private GameObject fracturedRock;

    [SerializeField]
    private float minDistance = .2f;
    [SerializeField]
    private float maxTime = 1f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 distance;

    private float startTime;
    private float endTime;
    [SerializeField]
    private float jumpStrength = 1;
    [SerializeField]
    private float yJump;

    public float maxDistance;
    private float coefficient;
    private float coefX;
    private float coefZ;

    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool canStop;
    [SerializeField]
    private bool isJumping;
    public bool canStab = true;

    private float fallStartTime = 0;
    public float maxFallTime = 5f;

    [SerializeField]
    public static bool isGameStarted;
    [SerializeField]
    public static bool isGameOver;

    [SerializeField]
    private AudioSource jumpingSFX;
    [SerializeField]
    private AudioSource stabbingSFX;
    [SerializeField]
    private AudioSource rockCrashSFX;
    [SerializeField]
    private AudioSource damageSFX;
    [SerializeField]
    private AudioSource staminaCollectSFX;
    [SerializeField]
    private AudioSource deathSFX;
    [SerializeField]
    private AudioSource bGMusic;

    public Vector3 lastPos;
    public Vector3 lastChildPos;

    private void Awake()
    {
        swipeAndTapManager = GetComponent<SwipeAndTapManager>();
    }

    private void OnEnable()
    {
        swipeAndTapManager.OnStartTouch += SwipeStart;
        swipeAndTapManager.OnEndTouch += SwipeEnd;
        swipeAndTapManager.OnTap += Tap;
    }

    private void OnDisable()
    {
        swipeAndTapManager.OnStartTouch -= SwipeStart;
        swipeAndTapManager.OnEndTouch -= SwipeEnd;
        swipeAndTapManager.OnTap -= Tap;
    }

    void Start()
    {
        startCanvas.SetActive(true);

        isGameStarted = false;
        isGameOver = false;

        staminaController = GetComponent<StaminaController>();
        staminaController.staminaBar.enabled = false;
        staminaController.staminaBarBG.enabled = false;

        cameraShakeScript = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShakeScript>();

        cameraController = GameObject.FindGameObjectWithTag("MainCameraParent").GetComponent<CameraController>();

        rockSpawner = GameObject.FindGameObjectWithTag("RockSpawner").GetComponent<RockSpawner>();
        rockSpawner.enabled = false;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        canJump = false;
        canStop = false;
    }

    private void SwipeStart(Vector3 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector3 position, float time)
    {
        if (isGameStarted)
        {
            endPosition = position;
            endTime = time;

            if (Vector3.Distance(startPosition, endPosition) >= minDistance && (endTime - startTime) <= maxTime)
            {
                distance = new Vector3((endPosition.x - startPosition.x), 0f, (endPosition.z - startPosition.z));
                canJump = true;
                //Debug.Log("Swipe");
                coefX = (endPosition.x - startPosition.x);
                coefZ = (endPosition.z - startPosition.z);
            }
        }
    }

    private void Tap(bool isTapped)
    {
        if (isGameStarted)
        {
            //Debug.Log("Tapped");
            if (isTapped && canStab)
            {
                canStop = true;
            }
        }
        /*else
        {
            isGameStarted = true;
            startCanvas.SetActive(false);
            gameplayCanvas.SetActive(true);
            rockSpawner.enabled = true;
            staminaController.staminaBar.enabled = true;
            staminaController.staminaBarBG.enabled = true;
        }*/
    }

    private void Update()
    {
        if (isGameStarted)
        {
            staminaController.DecreaseStamina();
        }

        transform.position = new Vector3(transform.position.x, 0.96f, transform.position.z);

        if (transform.position.x <= -11.4f)
        {
            transform.position = new Vector3(-11.4f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 11.4f)
        {
            transform.position = new Vector3(11.4f, transform.position.y, transform.position.z);
        }

        if (!isGameOver && isJumping && (Time.time - fallStartTime) > maxFallTime)
        {
            CharacterFall();
        }
    }

    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(90f, 0f, 0f);

        if (isGameStarted && cameraController.isCharacterAlive)
        {
            if (canJump && !isJumping)
            {
                fallStartTime = Time.time;

                //Kinematic is false so the gravity is on during jumping
                GetComponent<Rigidbody>().isKinematic = false;

                //Setting v to zero in case there is a remaining force
                if (rb.velocity != Vector3.zero)
                {
                    rb.velocity = Vector3.zero;
                }

                coefficient = Mathf.Sqrt((Mathf.Pow(maxDistance, 2)) / ((Mathf.Pow(coefZ, 2)) + (Mathf.Pow(coefX, 2))));
                //coefficient = Mathf.Sqrt((Mathf.Pow(maxDistance, 2)) / ((Mathf.Pow((endPosition.z - startPosition.z), 2)) + (Mathf.Pow((endPosition.x - startPosition.x), 2))));

                /*if (distance.magnitude > maxDistance)
                {
                    rb.velocity = new Vector3(distance.x, 0f, distance.z) * jumpStrength * coefficient;
                }
                else
                {
                    rb.velocity = new Vector3(distance.x, 0f, distance.z) * jumpStrength;
                }*/

                rb.velocity = new Vector3(distance.x, 0f, distance.z) * jumpStrength * coefficient;

                GetComponentInChildren<Animator>().Play("characterClimbAnim");

                jumpingSFX.Play();

                character.transform.localPosition += new Vector3(0f, yJump, 0f);

                canJump = false;
                isJumping = true;
            }
            else if (canStop && !canJump && isJumping && !rb.isKinematic/* && rb.velocity != Vector3.zero*/)
            {
                //Kinematic is true so the character stops when user taps
                rb.isKinematic = true;

                GetComponentInChildren<Animator>().Play("characterHoldAnim");

                stabbingSFX.Play();

                isJumping = false;
                canStop = false;
            }
            else if (canJump && isJumping)
            {
                //Character cannot do the jumping action while it is jumping
                canJump = false;
            }
            else if (canStop && !isJumping)
            {
                //Character cannot do the stopping action when it's stopped
                canStop = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NotStabbableSurface")
        {
            canStab = false;
        }
        else if (other.tag == "StaminaPotion")
        {
            staminaController.StaminaPotionIncrease();
            staminaCollectSFX.Play();
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else if (other.tag == "RockSpawnRaters")
        {
            rockSpawner.rate -= 0.1f;
            rockSpawner.changeRate = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "NotStabbableSurface")
        {
            canStab = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NotStabbableSurface")
        {
            canStab = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            staminaController.RockDamage();
            cameraShakeScript.CamShake();

            rockCrashSFX.Play();
            damageSFX.PlayDelayed(.2f);

            GameObject fracturedRockObject = Instantiate(fracturedRock, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            fracturedRockObject.transform.localScale = (collision.gameObject.transform.localScale / 80);
            
            Destroy(collision.gameObject);
        }
    }

    public void CharacterFall()
    {
        if (isJumping)
        {
            if ((Time.time - fallStartTime) > maxFallTime)
            {
                cameraController.isCharacterAlive = false;
                GetComponentInChildren<Animator>().Play("characterJumpingFallAnim");
                deathSFX.Play();
                bGMusic.Pause();
                rb.isKinematic = false;
                //canStop = false;
                gameplayCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
                rockSpawner.enabled = false;
                isGameOver = true;
                /*lastPos = gameObject.transform.position;
                lastChildPos = gameObject.transform.GetChild(0).gameObject.transform.position;*/
            }
            else if(staminaController.characterStamina <= 0)
            {
                cameraController.isCharacterAlive = false;
                GetComponentInChildren<Animator>().Play("characterJumpingFallAnim");
                deathSFX.Play();
                bGMusic.Pause();
                rb.isKinematic = false;
                gameplayCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
                rockSpawner.enabled = false;
                isGameOver = true;
            }
        }
        else
        {
            cameraController.isCharacterAlive = false;
            GetComponentInChildren<Animator>().Play("characterFallAnim");
            deathSFX.Play();
            bGMusic.Pause();
            rb.isKinematic = false;
            //canStop = false;
            gameplayCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            rockSpawner.enabled = false;
            isGameOver = true;
        }
        StartCoroutine(WaitAfterDead());
    }

    IEnumerator WaitAfterDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    /*public IEnumerator ReviveDelay()
    {
        yield return new WaitForSeconds(0.01f);

        gameObject.SetActive(true);
        cameraController.isCharacterAlive = true;
        rb.isKinematic = true;
        isGameStarted = true;
        isGameOver = false;
        rockSpawner.enabled = true;
        staminaController.staminaBar.enabled = true;
        staminaController.staminaBarBG.enabled = true;
        if (staminaController.characterStamina < 50f)
        {
            staminaController.characterStamina = 50f;
        }

        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, (lastPos.z + lastChildPos.y));
    }*/

    public void TapToStart()
    {
        StartCoroutine(WaitBeforeStart());
    }

    IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(0.3f);

        bGMusic.volume = 0.5f;

        isGameStarted = true;
        startCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
        canvas.GetComponent<StartMenu>().soundButtons.SetActive(false);
        canvas.GetComponent<StartMenu>().instructionsBtn.SetActive(false);
        rockSpawner.enabled = true;
        staminaController.staminaBar.enabled = true;
        staminaController.staminaBarBG.enabled = true;
    }
}
