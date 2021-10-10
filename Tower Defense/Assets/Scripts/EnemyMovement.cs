using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    public float startSpeed;
    private GameObject targetTile = null;

    // countdown for slowing movement speed
    [SerializeField] public float slowTime = 2f;
    public float slowTimeRemaining;

    // Information on map
    public MapGenerator map;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        setStartTile();
        startSpeed = movementSpeed;
        slowTimeRemaining = slowTime;
    }

    private void setStartTile()
    {
        map = FindObjectOfType<MapGenerator>();
        targetTile = map.startTile;
    }

    private void Update()
    {
        // Check if end reached
        checkPosition();
        moveEnemy();

        // Reset speed to original movement speed after a delay
        if (slowTimeRemaining > 0f)
        {
            slowTimeRemaining -= Time.deltaTime;
        }
        else
        {
            movementSpeed = startSpeed;
        }
    }

    private void moveEnemy()
    {
        /*transform.position = Vector3.MoveTowards(
            transform.position, targetTile.transform.position,
            movementSpeed * Time.deltaTime);
        */
        Vector3 dir = targetTile.transform.position - transform.position;
        transform.Translate(dir.normalized * movementSpeed * Time.deltaTime, Space.World);
    }

    private void checkPosition()
    {
        if (targetTile != null && targetTile != map.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if (distance < 0.001f)
            {
                // set target tile to next tile
                int currentIndex = map.pathTiles.IndexOf(targetTile) + 1;

                targetTile = map.pathTiles[currentIndex + 1];
            }
        }

        // reached end of map
        if (gameObject.transform.position == map.endTile.transform.position)
        {
            enemy.reachedEnd();
        }
    }

    public void Slow(float percent)
    {
        movementSpeed = startSpeed * (1f - percent);
        slowTimeRemaining = slowTime;
    }
}
