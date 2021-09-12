using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenShorts; // in seconds

    private float nextTimeToShoot;

    public GameObject currentTarget;

    private void Start()
    {
        nextTimeToShoot = Time.time;
    }

    // Detect nearest enemy to set target
    private void updateNearestEnemy()
    {
        GameObject currentNE = null;
        float distance = Mathf.Infinity;

        // find nearest enemy
        foreach(GameObject enemy in Enemies.enemies )
        {
            if(enemy != null)
            {
                float _distance = (transform.position - enemy.transform.position).magnitude;
                if (_distance < distance)
                {
                    distance = _distance;
                    currentNE = enemy;
                }
            }
        }

        // if nearest enemy is within range, set as target
        if( distance <= range )
        {
            currentTarget = currentNE;
        }
        else
        {
            currentTarget = null;
        }
    }

    protected virtual void shoot()
    {
        Enemy enemy = currentTarget.GetComponent<Enemy>();
        enemy.takeDamage(damage);
    }

    private void Update()
    {
        updateNearestEnemy();

        // if there is a valid target and 
        if(Time.time >= nextTimeToShoot && currentTarget != null)
        {
            shoot();
            nextTimeToShoot = Time.time + timeBetweenShorts;
        }
    }
}
