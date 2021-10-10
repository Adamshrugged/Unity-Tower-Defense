using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCountdown = 0f;
    [SerializeField] public float damageOverTime = 0f;
    [SerializeField] public float damageOverTimeDuration = 0f;
    [SerializeField] public float slowPercent = 1f;

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
                // Ignore any destroyed enemies in the list
                if (enemy != null)
                {
                    // get distance to enemy
                    float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distToEnemy < shortestDistance)
                    {
                        enemyNearest = enemy;
                        shortestDistance = distToEnemy;
                    }
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

    protected virtual void shoot(GameObject target, float damage, float damageOverTime, 
        float damageOverTimeDuration, float slowPercent)
    {
        Enemy enemy = currentTarget.GetComponent<Enemy>();
    }

    private void rotateTurret()
    {
        // Ensure there is a valid target and the rotation pivot are set
        if( pivot != null & currentTarget != null )
        {
            // Rotate barrel
            Vector2 dir = currentTarget.transform.position - pivot.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Vector3 newRotation = new Vector3(0, 0, angle);
            pivot.localRotation = Quaternion.Euler(newRotation);
        }
    }

    private void Update()
    {

        // Rotate barrel towards the enemy
        rotateTurret();

        // Shoot if there is a valid target
        // and sufficient time passed since last shot
        if (currentTarget != null && fireCountdown <= 0f)
        {
            shoot(currentTarget, damage, damageOverTime, damageOverTimeDuration, slowPercent);
            fireCountdown = 1 / fireRate;
        }

        // Decrease countdown
        fireCountdown -= Time.deltaTime;
    }
}
