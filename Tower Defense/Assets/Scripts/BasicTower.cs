using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    // Add a bullet
    [SerializeField] public GameObject bullet;

    [SerializeField] public Transform barrel;
    [SerializeField] public Transform pivot;

    protected override void shoot()
    {
        base.shoot();
        GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
    }
}
