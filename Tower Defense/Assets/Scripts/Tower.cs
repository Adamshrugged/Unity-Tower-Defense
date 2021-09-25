using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenShorts; // in seconds

    // Specifics for each tower
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform barrel;
    [SerializeField] public Transform pivot;

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

    protected virtual void shoot(GameObject target)
    {
        Enemy enemy = currentTarget.GetComponent<Enemy>();
    }

    private void Update()
    {
        // Update enemy list if necessary
        if(currentTarget == null)
        {
            updateNearestEnemy();
        }

        // Rotate barrel towards the enemy
        // Ensure there is a valid target and begin rotating barrel towards it
        if (pivot!= null && currentTarget != null)
        {
            Vector2 relative = currentTarget.transform.position - pivot.position;
            float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

            Vector3 newRotation = new Vector3(0, 0, angle);
            pivot.localRotation = Quaternion.Euler(newRotation);
        }

        // Check time
        //Debug.Log("Time1: " + Time.time + ". Time2: " + nextTimeToShoot);

        // Shoot if there is a valid target
        // if there is a valid target and sufficient time passed since last shot
        if (Time.time >= nextTimeToShoot && currentTarget != null )
        {
            shoot(currentTarget);
            nextTimeToShoot = Time.time + timeBetweenShorts;
        }
    }
}
