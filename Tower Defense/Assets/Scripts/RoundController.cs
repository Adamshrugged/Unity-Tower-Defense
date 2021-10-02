using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] public GameObject parentGameObject;
    [SerializeField] public GameObject basicEnemy;

    [SerializeField] public float timeBetweenWaves;
    [SerializeField] public float timeBeforeRoundStarts;

    [SerializeField] public UIController uiController;

    int round;
    public float timeVariable;
    private float countdown;

    private void Start()
    {
        round = 0;
    }

    private void SpawnEnemies()
    {
        StartCoroutine("ISpawnEnemies");
    }
    IEnumerator ISpawnEnemies()
    {
        Vector3 newPos = MapGenerator.startTile.transform.position;
        Vector3 finalPos;

        for ( int i=0; i<round; i++)
        {
            finalPos = newPos;
            // Add some randomness to location
            finalPos.x += Random.Range(-1.1f, 1.1f);
            finalPos.y += 1f;

            GameObject newEnemy = Instantiate(basicEnemy, finalPos, Quaternion.identity,
                parentGameObject.transform);
            //newEnemy.transform.position = finalPos;
            yield return new WaitForSeconds(1f);
        }
    }


    private void Update()
    {
        // Change countdown
        countdown -= Time.deltaTime;

        // When countdown reaches zero, send wave and reset counter
        if(countdown <= 0f)
        {
            countdown = timeBetweenWaves;
            round++;
            uiController.setWave(round);
            SpawnEnemies();
        }

        // Update UI
        uiController.setCountdown(countdown);
    }
}
