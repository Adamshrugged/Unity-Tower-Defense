using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    [SerializeField] public Text roundsText;
    [SerializeField] public float textPause = 0.5f;


    private void OnEnable()
    {
        //roundsText.text = PlayerStats.Rounds.ToString();
        StartCoroutine(AnimateText());
    }

    // count up to # of rounds survived with small pause
    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        // wait before starting the countup
        yield return new WaitForSeconds(0.7f);

        while(round < PlayerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();
            yield return new WaitForSeconds(textPause);
        }

    }
}
