using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] public Text scoreText;
    [SerializeField] public Text moneyText;
    [SerializeField] public Text waveText;
    [SerializeField] public Text healthText;
    [SerializeField] public Text countdownText;

    // Starting money
    [SerializeField] private int startingMoney;

    private int score;
    private int money;
    private int wave;
    private int health;
    private int countdown;

    private void Awake()
    {
        // Starting values
        score = 0;
        money = startingMoney;
        wave = 1;
        health = 1;
        countdown = 0;

        // set intial values in UI
        scoreText.text = score.ToString();
        moneyText.text = money.ToString();
        waveText.text = wave.ToString();
        healthText.text = health.ToString();
        countdownText.text = countdown.ToString();
    }

    public void setScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    public void setMoney(int m)
    {
        money += money;
        moneyText.text = money.ToString();
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
