using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{   
    public Text scoreText;
    public Text highScoreText;
    public static int score = 0;
    int highScore = 0;
    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if(highScore < score)
            PlayerPrefs.SetInt("highScore", score);
    }
}