using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int bulletSpeed;
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
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);

            // Destroy bullet
            Destroy(gameObject);
        }
    }
}
