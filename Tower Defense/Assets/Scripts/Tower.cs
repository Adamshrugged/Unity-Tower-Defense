using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenShorts; // in seconds
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float fireCountdown = 0f;

    // Specifics for each tower
    [Header("Tower Parts")]
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform barrel;
    [SerializeField] public Transform pivot;

    private float nextTimeToShoot;

    public GameObject currentTarget;

    // Debug information
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {
        nextTimeToShoot = Time.time;

        // Only call update target every half second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        /*
         * Potential uses:
         *  -Nearest enemy
         *  -Closest enemy
         *  -Healthiest enemy
         *  -Most armored enemy
         *  -Most shielded enemy
         */
        float shortestDistance = Mathf.Infinity;
        GameObject enemyNearest = null;

        // Loop through enemies to find options
        if (currentTarget == null)
        {
            // Loop through enemies in scene
            foreach (GameObject enemy in Enemies.enemies)
            {
                // get distance to enemy
                float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distToEnemy < shortestDistance)
                {
                    enemyNearest = enemy;
                    shortestDistance = distToEnemy;
                }
            }
        }

        // Set target
        if(enemyNearest != null && shortestDistance <= range)
        {
            currentTarget = enemyNearest;
        }
        else
        {
            currentTarget = null;
        }
    }

    protected virtual void shoot(GameObject target, float damage)
    {
        Enemy enemy = currentTarget.GetComponent<Enemy>();
    }

    private void rotateTurret()
    {
        // Ensure there is a valid target and the rotation pivot are set
        if( pivot != null & currentTarget != null )
        {
            // One method to rotate barrel
            Vector2 dir = currentTarget.transform.position - pivot.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Vector3 newRotation = new Vector3(0, 0, angle);
            pivot.localRotation = Quaternion.Euler(newRotation);

            // Second method to rotate barrel - not working
            /*Vector3 dir = currentTarget.transform.position - pivot.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(pivot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            pivot.rotation = Quaternion.Euler(0f, 0f, rotation.z);
            Debug.Log(rotation.z);*/
        }
    }

    private void Update()
    {

        // Rotate barrel towards the enemy
        rotateTurret();

        // Check time
        //Debug.Log("Time1: " + Time.time + ". Time2: " + nextTimeToShoot);

        // Shoot if there is a valid target
        // if there is a valid target and sufficient time passed since last shot
        /*if (Time.time >= nextTimeToShoot && currentTarget != null )
        {
            shoot(currentTarget);
            nextTimeToShoot = Time.time + timeBetweenShorts;
        }*/
        if (currentTarget != null && fireCountdown <= 0f)
        {
            shoot(currentTarget, damage);
            fireCountdown = 1 / fireRate;
        }

        // Decrease countdown
        fireCountdown -= Time.deltaTime;
    }
}
