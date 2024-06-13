using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject character;

    public GameObject cameraLimiterPlane;

    public Vector3 offset;

    public float smoothness;

    public bool isCharacterAlive = true;

    private void Start()
    {
        isCharacterAlive = true;
    }

    private void Update()
    {
        cameraLimiterPlane.transform.position = new Vector3(cameraLimiterPlane.transform.position.x, cameraLimiterPlane.transform.position.y,
            transform.position.z);
    }

    private void FixedUpdate()
    {
        if (CharacterControllerScript.isGameStarted)
        {
            if (isCharacterAlive)
            {
                Vector3 desiredPosition = character.transform.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);
                transform.position = smoothedPosition;

                if (transform.position.x <= -7.2f)
                {
                    transform.position = new Vector3(-7.2f, transform.position.y, transform.position.z);
                }
                else if (transform.position.x >= 7.2f)
                {
                    transform.position = new Vector3(7.2f, transform.position.y, transform.position.z);
                }

                if (transform.position.z <= 10f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
                }
            }
        }
    }
}
