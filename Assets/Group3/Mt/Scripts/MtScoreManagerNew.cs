using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MtScoreManagerNew : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int currentScore = 0;

    public void AddScore(int points)
    {
        currentScore += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
        //Debug.Log($"Score added: {points}, Current Score: {currentScore}");

    }
}