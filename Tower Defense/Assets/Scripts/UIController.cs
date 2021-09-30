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

    // Starting money
    [SerializeField] private int startingEnergy;

    private int score;
    private int energy;
    private int wave;
    private int health;
    private int countdown;

    private void Awake()
    {
        // Starting values
        score = 0;
        energy = startingEnergy;
        wave = 1;
        health = 1;
        countdown = 0;

        // set intial values in UI
        scoreText.text = score.ToString();
        energyText.text = energy.ToString();
        waveText.text = wave.ToString();
        healthText.text = health.ToString();
        countdownText.text = countdown.ToString();
    }

    public void setScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    public void setEnergy(int m)
    {
        energy += energy;
        energyText.text = energy.ToString();
    }
    public void setWave(int w)
    {
        wave = w;
        waveText.text = wave.ToString();
    }
    public void setHealth(int healthAdjust)
    {
        health += healthAdjust;
        healthText.text = health.ToString();
    }
    public void setCountdown(int c)
    {
        countdown = c;

        if(c == 0)
        {
            countdownText.text = "";
        }
        else
        {
            countdownText.text = countdown.ToString();
        }

        // if less than 5, add emphasis
        if(c < 5 && c > 0)
        {
            countdownText.color = Color.red;
        }
    }
}
