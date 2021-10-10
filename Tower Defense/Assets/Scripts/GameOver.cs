using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] public Text roundsText;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        // reloads currently loaded scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("TDGameScene");
    }

    public void Menu ()
    {
        Debug.Log("go to menu");
    }
}
