using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] public sceneFader sceneFader;
    [SerializeField] string menuScene = "TDGameMenu";

    public void Retry()
    {
        // reloads currently loaded scene
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu ()
    {
        sceneFader.FadeTo(menuScene);
    }
}
