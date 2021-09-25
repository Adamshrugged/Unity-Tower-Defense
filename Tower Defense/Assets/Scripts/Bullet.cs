using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletDamage = 1.0f;
    [SerializeField] float bulletSpeed = 1.0f;
    public GameObject target = null;

    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = dir.normalized * (int)bulletSpeed*10;
        }
        else
        {
            transform.position += transform.right * bulletSpeed * 0.01f;
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
            collision.gameObject.GetComponent<Enemy>().takeDamage(bulletDamage);
        }

        // Destroy bullet
        Destroy(gameObject);
    }
}
