using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public float score;
    private float highScore;

    private void OnEnable()
    {
        PlayerHealth.OnDeath += PlayerHealth_OnDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDeath -= PlayerHealth_OnDeath;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
        }

        highScoreText.text = "High Score: " + Round(highScore, 2);
    }

    private void PlayerHealth_OnDeath(object sender, PlayerHealth.OnDeathEventArgs e)
    {
        if (e.DeadPlayer.isAlive == false)
        {
            //set high score if it is higher
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetFloat("HighScore", highScore);
            }
        }
    }

    private void Update()
    {
        score += Time.deltaTime;
        scoreText.text = "Score: " + Round(score, 2);
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}
