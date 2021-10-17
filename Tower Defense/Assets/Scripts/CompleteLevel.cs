using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] public sceneFader sceneFader;
    [SerializeField] string menuScene = "TDGameMenu";

    public string nextLevel = "TD-Level-02";
    public int levelToUnlock = 2;

    // Transition to next scene
    public void Continue()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);

        if (PlayerPrefs.GetInt("levelReached") < levelToUnlock)
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }

        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuScene);
    }
}
