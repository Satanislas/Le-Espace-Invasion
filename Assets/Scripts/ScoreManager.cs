using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;
    public TextMeshProUGUI highScoreText;
    public int highScore;

    public static ScoreManager scoreManager; //singleton
    
    private void Start()
    {
        scoreManager = this;
        highScore = PlayerPrefs.GetInt("highscore");
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = score.ToString("000000");
        highScoreText.text = highScore.ToString("000000");
    }

    public void addScore(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore",highScore);
        }
        UpdateText();
    }
}
