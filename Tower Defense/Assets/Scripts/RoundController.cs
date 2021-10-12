using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] public GameObject parentGameObject;

    [SerializeField] public float timeBetweenWaves;
    [SerializeField] public float timeBeforeRoundStarts;

    [SerializeField] public UIController uiController;

    [SerializeField] public Wave[] waves;

    public static int EnemiesAlive;

    // Information on map
    MapGenerator map;

    int round;
    public float timeVariable;
    private float countdown;

    private bool sendNextWave = false;

    private void Start()
    {
        round = 0;

        // set map
        map = FindObjectOfType<MapGenerator>();
    }

    private void SpawnEnemies()
    {
        StartCoroutine("ISpawnEnemies");
    }
    IEnumerator ISpawnEnemies()
    {
        Wave wave = waves[round - 1];

        for (int i = 0; i < wave.count; i++)
        {
            spawnEnemy(wave.enemy, map.startTile.transform.position);

            yield return new WaitForSeconds(1f / wave.rate);
        }
    }

    private void spawnEnemy(GameObject enemy, Vector3 position)
    {
        // Add some randomness to spawn location
        Vector3 finalPos = position;
        finalPos.x += Random.Range(-1.1f, 1.1f);
        finalPos.y += 1f;

        GameObject newEnemy = Instantiate(enemy, finalPos, Quaternion.identity,
                parentGameObject.transform);
        EnemiesAlive++;
    }


    private void Update()
    {
        // Check if end of round reached. Disables script
        if (round == waves.Length && EnemiesAlive == 0)
        {
            Debug.Log("You win!");
            this.enabled = false;
            return;
        }

        // Don't spawn until enemies from previous round have been killed
        if (EnemiesAlive > 0 && !sendNextWave)
        {
            return;
        }

        // Start with the time before waves are generated
        if(timeBeforeRoundStarts > 0f)
        {
            countdown += timeBeforeRoundStarts;
            timeBeforeRoundStarts = 0f;
        }

        // Change countdown
        countdown -= Time.deltaTime;

        // When countdown reaches zero, send wave and reset counter
        if(countdown <= 0f && round != waves.Length)
        {
            countdown = timeBetweenWaves;
            round++;
            PlayerStats.Rounds++;
            uiController.setWave(round);
            SpawnEnemies();
            sendNextWave = false;
        }

        // Update UI
        if (countdown >= 0f)
        {
            uiController.setCountdown(countdown);
        }
    }

    // Useful for immediately sending the next wave
    public void sendWave()
    {
        countdown = 0f;
        sendNextWave = true;
    }
}
