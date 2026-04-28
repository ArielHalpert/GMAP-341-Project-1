using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HighScore : MonoBehaviour
{   
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    int highScore = 0;


    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        RefreshDisplay();
    }
    
    void Update()
    {
        if (highScore < GameManager.score)
        {
            highScore = GameManager.score;
            PlayerPrefs.SetInt("highScore", highScore);
        }

        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        scoreText.text = "Score: " + GameManager.score.ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0).ToString();
    }
}