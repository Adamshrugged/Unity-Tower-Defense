using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private float movementSpeed;

    // Money rewarded to player upon death
    private int killReward;

    // Damage inflicted when end reached
    private int damage;

    private GameObject targetTile;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }

    private void Start()
    {
        initializeEnemy();
    }

    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    public void takeDamage(float amount)
    {
        enemyHealth -= amount;

        if(enemyHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, targetTile.transform.position, 
            movementSpeed * Time.deltaTime );
    }

    private void checkPosition()
    {
        if( targetTile != null && targetTile != MapGenerator.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if( distance < 0.001f)
            {
                // set target tile to next tile
                int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile) + 1;

                targetTile = MapGenerator.pathTiles[currentIndex + 1];
            }
        }
    }

    private void Update()
    {
        // Check if end reached
        checkPosition();
        moveEnemy();
    }
}
