using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    private float highScore;

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
        }

        highScoreText.text = "High Score: " + Round(highScore, 2);
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}
