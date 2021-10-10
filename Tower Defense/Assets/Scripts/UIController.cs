using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] public Text scoreText;
    [SerializeField] public Text energyText;
    [SerializeField] public Text waveText;
    [SerializeField] public Text healthText;
    [SerializeField] public Text countdownText;

    private int score;
    private int energy;
    private int wave;
    private float countdown;

    private void Awake()
    {
        // Starting values
        score = 0;
        energy = 0;
        wave = 0;
        countdown = 0;

        // set intial values in UI
        scoreText.text = score.ToString();
        energyText.text = energy.ToString();
        waveText.text = wave.ToString();
        setLives();
        countdownText.text = countdown.ToString();
    }

    private void Update()
    {
        energyText.text = PlayerStats.energy.ToString();
    }

    public void setScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    public void updateEnergy(int m)
    {
        energyText.text = m.ToString();
    }
    public void setWave(int w)
    {
        wave = w;
        waveText.text = wave.ToString();
    }
    public void setLives()
    {
        healthText.text = PlayerStats.lives.ToString();
    }
    public void setCountdown(float c)
    {
        countdown = c;

        countdownText.text = string.Format("{0:0.0}", countdown);

        // if less than 5, add emphasis
        countdownText.color = Color.black;
        if (c < 5 && c > 0)
        {
            countdownText.color = Color.red;
        }
    }
}
