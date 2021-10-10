using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneFader : MonoBehaviour
{
    [SerializeField] public Image img;
    [SerializeField] public AnimationCurve fadeCurve;
    [SerializeField] public float startTime = 1f;
    private float time;

    private void Start()
    {
        time = startTime;
        StartCoroutine( FadeIn() );
    }

    public void FadeTo(string scene)
    {
        StartCoroutine( FadeOut(scene) );

    }

    public IEnumerator FadeIn()
    {
        while (time > 0f)
        {
            time -= Time.deltaTime;
            float a = fadeCurve.Evaluate(time);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;         // Skip to next frame
        }
    }

    public IEnumerator FadeOut(string scene)
    {
        while (time < startTime)
        {
            time += Time.deltaTime;
            float a = fadeCurve.Evaluate(time);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;         // Skip to next frame
        }

        SceneManager.LoadScene(scene);
    }
}
