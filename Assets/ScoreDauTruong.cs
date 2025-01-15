using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDauTruong : MonoBehaviour
{
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtHighScore;
    private int currentScore;
    private int highScore;
    void Start()
    {
        txtScore.text = "Score: 0";
        PlayerPrefs.SetInt("PlayDauTruong", 1);
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        highScore = PlayerPrefs.GetInt("HighScore");
        txtHighScore.text = "High Score: " + highScore;
        currentScore = 0;
        PlayerPrefs.SetInt("CurrentScore", 0);
        PlayerPrefs.Save();
    }
    void Update()
    {
        currentScore = PlayerPrefs.GetInt("CurrentScore");
        txtScore.text = "Score: " + currentScore;
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScore = currentScore;
            txtHighScore.text = "High Score: " + highScore;
            PlayerPrefs.Save();
        }
    }
}
