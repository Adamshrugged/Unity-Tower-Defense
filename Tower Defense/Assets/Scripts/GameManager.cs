using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver = false;
    [SerializeField] public GameObject gameOverUI;
    [SerializeField] public GameObject completeLevelUI;

    private void Start()
    {
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsOver)
        {
            return;
        }

        if(Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if(PlayerStats.lives <= 0)
        {
            // Player is out of lives
            EndGame();
        }
    }

    // Losing the game
    private void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

    // Display the "complete level" screen
    public void WinLevel()
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
