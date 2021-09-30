using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int bulletSpeed;
    [SerializeField] float explosionRadius = 0f;
    public GameObject target = null;
    public float damage;
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
            // Check if there is splash damage
            if(explosionRadius > 0f)
            {
                Explode(collision.gameObject);
            }
            // If no splash damage, apply damage directly
            else
            {
                Damage(collision.gameObject);
                //collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            }

            // Destroy bullet
            Destroy(gameObject);
        }
    }

    // Apply damage to multiple targets
    private void Explode(GameObject target)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider collider in targets)
        {
            if(collider.tag == "Enemy")
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.red;
    }
}
