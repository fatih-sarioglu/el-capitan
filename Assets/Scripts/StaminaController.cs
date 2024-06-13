using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float characterStamina = 100f;
    [SerializeField]
    private float maxStamina = 100f;
    [SerializeField]
    private float rockDamageAmount = 1f;
    [SerializeField]
    private float staminaPotionAmount = 1f;
    [HideInInspector] public bool hasRegenerated = true;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)] [SerializeField] private float staminaDrain = 0.05f;
    [Range(0, 50)] [SerializeField] private float staminaRegen = 0.05f;

    [Header("Stamina UI Elements")]
    [SerializeField] public Image staminaBar = null;
    [SerializeField] public Image staminaBarBG = null;

    private CharacterControllerScript characterControllerScript;

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private GameObject gameplayCanvas;

    private void Start()
    {
        characterControllerScript = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>();
    }

    public void RegenStamina()
    {
        if (characterStamina <= maxStamina - 0.01f)
        {
            characterStamina += staminaRegen * Time.deltaTime;
            UpdateStaminaBar();

            if (characterStamina >= maxStamina)
            {
                hasRegenerated = true;
            }

        }
    }

    public void DecreaseStamina()
    {
        if (characterStamina >= 0)
        {
            characterStamina -= staminaDrain * Time.deltaTime;
            UpdateStaminaBar();

            if (characterStamina <= 0)
            {
                Debug.Log("Zed's dead baby Zed's dead");
                characterControllerScript.CharacterFall();
                /*gameplayCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
                CharacterControllerScript.rockSpawner.enabled = false;
                CharacterControllerScript.isGameOver = true;
                characterControllerScript.gameObject.SetActive(false);*/
            }

        }
    }

    public void RockDamage()
    {
        if (characterStamina >= 0)
        {
            characterStamina -= rockDamageAmount;
            UpdateStaminaBar();

            if (characterStamina <= 0)
            {
                Debug.Log("Zed's dead baby Zed's dead");
                characterControllerScript.CharacterFall();
                /*gameplayCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
                CharacterControllerScript.rockSpawner.enabled = false;
                CharacterControllerScript.isGameOver = true;
                characterControllerScript.gameObject.SetActive(false);*/
            }

        }
    }

    public void StaminaPotionIncrease()
    {
        if (characterStamina >= 0)
        {
            characterStamina += staminaPotionAmount;
            
            if (characterStamina >= maxStamina)
            {
                characterStamina = maxStamina;
            }

            UpdateStaminaBar();
        }
    }

    private void UpdateStaminaBar()
    {
        staminaBar.fillAmount = characterStamina / maxStamina;
    }
}
