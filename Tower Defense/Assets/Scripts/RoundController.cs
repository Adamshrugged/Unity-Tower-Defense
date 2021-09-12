using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RoundState
{
    ROUND,
    INTERMISSION,
    START
}

public class RoundController : MonoBehaviour
{
    [SerializeField] public GameObject basicEnemy;

    [SerializeField] public float timeBetweenWaves;
    [SerializeField] public float timeBeforeRoundStarts;

    int round;
    public float timeVariable;

    private RoundState state;

    private void Start()
    {
        state = RoundState.START;
        round = 1;

        // Designate when round will start
        timeVariable = Time.time + timeBeforeRoundStarts;
    }

    private void SpawnEnemies()
    {
        StartCoroutine("ISpawnEnemies");
    }
    IEnumerator ISpawnEnemies()
    {
        for( int i=0; i<round; i++)
        {
            GameObject newEnemy = Instantiate(basicEnemy, 
                MapGenerator.startTile.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }


    private void Update()
    {
        if(state == RoundState.START)
        {
            if(Time.time >= timeVariable)
            {
                state = RoundState.ROUND;
                SpawnEnemies();
                return;
            }
        }
        else if(state == RoundState.INTERMISSION)
        {
            if (Time.time >= timeVariable)
            {
                state = RoundState.ROUND;
                SpawnEnemies();
                return;
            }
        }
        else if (state == RoundState.ROUND)
        {
            // Check amount of enemies remaining
            if(Enemies.enemies.Count > 0)
            {
                //
            }
            else
            {
                // intermission after killing enemies
                state = RoundState.INTERMISSION;
                timeVariable = Time.time + timeBetweenWaves;
                round++;
            }
        }
    }
}
