using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject soundOnBtn;
    [SerializeField]
    private GameObject soundOffBtn;
    [SerializeField]
    public GameObject soundButtons;

    [SerializeField]
    private GameObject settingsCanvasGroup;
    [SerializeField]
    private GameObject startCanvasGroup;
    [SerializeField]
    private GameObject creditsUI;
    [SerializeField]
    private GameObject how2PlayUI;

    [SerializeField]
    public GameObject instructionsBtn;

    [SerializeField]
    private AudioSource bGMusic;

    public static bool isMusicOn;

    private void Start()
    {
        soundButtons.SetActive(true);
        instructionsBtn.SetActive(true);

        if (PlayerPrefs.GetInt("IsMusicOn") == 1)
        {
            bGMusic.Play();
            soundOffBtn.SetActive(true);
            soundOnBtn.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("IsMusicOn") == 0)
        {
            bGMusic.Pause();
            soundOnBtn.SetActive(true);
            soundOffBtn.SetActive(false);
        }
    }

    public void Mute()
    {
        bGMusic.Pause();
        isMusicOn = false;
        PlayerPrefs.SetInt("IsMusicOn", 0);
        soundOffBtn.SetActive(false);
        soundOnBtn.SetActive(true);
    }

    public void SoundOn()
    {
        bGMusic.Play();
        isMusicOn = true;
        PlayerPrefs.SetInt("IsMusicOn", 1);
        soundOffBtn.SetActive(true);
        soundOnBtn.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        startCanvasGroup.gameObject.SetActive(false);
        settingsCanvasGroup.SetActive(true);
        soundButtons.SetActive(true);
        instructionsBtn.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        startCanvasGroup.gameObject.SetActive(true);
        settingsCanvasGroup.SetActive(false);
        soundButtons.SetActive(true);
        instructionsBtn.SetActive(true);
    }

    public void OpenCredits()
    {
        settingsCanvasGroup.SetActive(false);
        soundButtons.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void CloseCredits()
    {
        settingsCanvasGroup.SetActive(true);
        soundButtons.SetActive(true);
        creditsUI.SetActive(false);
    }

    public void OpenInstructions()
    {
        startCanvasGroup.SetActive(false);
        how2PlayUI.SetActive(true);
        soundButtons.SetActive(false);
        instructionsBtn.SetActive(false);
    }

    public void CloseInstructions()
    {
        startCanvasGroup.SetActive(true);
        how2PlayUI.SetActive(false);
        soundButtons.SetActive(true);
        instructionsBtn.SetActive(true);
    }
}
