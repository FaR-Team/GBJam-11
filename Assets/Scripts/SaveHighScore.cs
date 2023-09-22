#pragma warning disable CS0108
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHighScore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI name;
    public int currentScore;

    // Update is called once per frame
    void Update()
    {
        score.text = $"Score: {PlayerPrefs.GetInt("highscore")}";
    }

    public void SendScore()
    {
        if (currentScore > PlayerPrefs.GetInt("highscore") || PlayerPrefs.GetInt("highscore") == 0)
        {
            PlayerPrefs.SetInt("highscore", currentScore);
            HighScores.UploadScore(name.text, currentScore);
        }
    }
}
