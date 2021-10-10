using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //
    [SerializeField] public GameObject ui;
    [SerializeField] public sceneFader sceneFader;
    [SerializeField] string menuScene = "TDGameMenu";

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    // Pause game - stop creeps etc and 
    public void Toggle()
    {
        // Invert active status
        ui.SetActive( !ui.activeSelf );

        // If UI is active, free game
        if(ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo( SceneManager.GetActiveScene().name );
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuScene);
    }
}
