using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameplayUI;
    public GameObject settingsMenuUI;
    public GameObject creditsUI;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject instructionsBtn;

    [SerializeField]
    private GameObject how2PlayUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;
        instructionsBtn.SetActive(false);
        canvas.GetComponent<StartMenu>().soundButtons.SetActive(false);
    }

    public void Pause()
    {
        gameplayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        instructionsBtn.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OpenSettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        canvas.GetComponent<StartMenu>().soundButtons.SetActive(true);
        instructionsBtn.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        canvas.GetComponent<StartMenu>().soundButtons.SetActive(false); 
        instructionsBtn.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        canvas.GetComponent<StartMenu>().soundButtons.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        canvas.GetComponent<StartMenu>().soundButtons.SetActive(true);
    }

    public void OpenInstructions()
    {
        pauseMenuUI.SetActive(false);
        how2PlayUI.SetActive(true);
        instructionsBtn.SetActive(false);
    }

    public void CloseInstructions()
    {
        pauseMenuUI.SetActive(true);
        how2PlayUI.SetActive(false);
        instructionsBtn.SetActive(true);
    }
}
