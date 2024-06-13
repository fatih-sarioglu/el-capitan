using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraForPos;

    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;
    [SerializeField]
    private TextMeshProUGUI scoreTMP;

    void Start()
    {
        scoreTMP.text = "0";
        //text.text = "0";

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterControllerScript.isGameStarted)
        {
            if ((((int)cameraForPos.transform.position.z) - 10) > int.Parse(scoreTMP.text))
            {
                scoreTMP.text = ((int)((cameraForPos.transform.position.z - 10))).ToString();
                //text.text = ((int)((cameraForPos.transform.position.z - 10))).ToString();
            }
            else
            {
                scoreTMP.text = scoreTMP.text;
                //text.text = text.text;
            }
        }

        if (CharacterControllerScript.isGameOver)
        {
            gameOverScoreText.text = ((int)((cameraForPos.transform.position.z - 10))).ToString();

            if ((((int)cameraForPos.transform.position.z) - 10) > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", (((int)cameraForPos.transform.position.z) - 10));
                highScoreText.text = "High Score: " + ((int)((cameraForPos.transform.position.z - 10))).ToString();
            }
        }
    }
}
