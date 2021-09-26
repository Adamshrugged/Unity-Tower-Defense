using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    protected override void shoot(GameObject target, float damage)
    {
        base.shoot(target, damage);
        GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
        newBullet.GetComponent<Bullet>().target = target;
        newBullet.GetComponent<Bullet>().damage = damage;
    }
}
