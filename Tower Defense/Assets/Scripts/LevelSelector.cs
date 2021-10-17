using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    // scene fader functionality
    public sceneFader fader;

    // Levels
    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        // loop through buttons to remove interactability
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if( i+1 > levelReached )
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
