using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    // Update is called once per frame
    void Update()
    {
        if(gameEnded)
        {
            return;
        }

        if(PlayerStats.lives <= 0)
        {
            // Player is out of lives
            EndGame();
        }
    }

    private void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game over!");
    }
}
