using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variables set by bullet
    [SerializeField] int bulletSpeed;
    [SerializeField] float explosionRadius = 0f;
    [SerializeField] GameObject impactEffect = null;
    
    // Variables passed by tower
    public float damageOverTime = 0f;
    public float damageOverTimeDuration = 0f;
    public float damage;
    public GameObject target = null;
    public float slowPercent = 1f;

    private bool moving = false;

    private void Update()
    {
        // Set movement if there is a target and bullet is not already moving
        if (target != null && !moving)
        {
            Vector3 direction = target.transform.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;

            // Change rotation to target
            float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            // set state of bullet
            moving = true;
        }
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Validate if bullet hit an enemy
        if(collision.gameObject.GetComponent<Enemy>() != null )
        {
            // Apply particle effect if defined
            if (impactEffect != null)
            {
                GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effectIns, 1f);
            }

            // Check if there is splash damage
            if(explosionRadius > 0f)
            {
                Explode(collision.gameObject);
            }
            // If no splash damage, apply damage directly
            else
            {
                Damage(collision.gameObject);
            }

            // Apply damage of time effects if defined
            if(damageOverTime > 0f && damageOverTimeDuration > 0f)
            {
                if (target != null)
                {
                    DamageOverTime(target, damageOverTime, damageOverTimeDuration);
                }
            }

            // Apply slow speed 
            if( slowPercent > 0f)
            {
                if (target != null)
                {
                    target.GetComponent<EnemyMovement>().Slow(slowPercent);
                }
            }

            // Destroy bullet
            Destroy(gameObject);
        }
    }

    // Apply damage to multiple targets
    private void Explode(GameObject target)
    {
        // Create circle around missile of size explosion radius to determine what objects are impacts
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // Cycle through each object and if it is an enemy, then apply damage
        foreach(Collider2D collider in targets)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                Damage(collider.gameObject);
            }
        }
    }

    // Apply damage to a single target
    private void Damage(GameObject target)
    {
        target.GetComponent<Enemy>().takeDamage(damage);
    }

    // Apply damage to a single target over time
    private void DamageOverTime(GameObject target, float damageOverTime, float damageOverTimeDuration)
    {
        target.GetComponent<Enemy>().takeDamageOverTime(damageOverTime, damageOverTimeDuration);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.red;
    }
}
