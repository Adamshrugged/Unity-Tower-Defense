using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    protected override void shoot(GameObject target, float damage, float damageOverTime, float damageOverTimeDuration, float slowPercent)
    {
        base.shoot(target, damage, damageOverTime, damageOverTimeDuration, slowPercent);
        GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);

        // Adjust bullet effects
        Bullet thisBullet = newBullet.GetComponent<Bullet>();
        thisBullet.target = target;
        thisBullet.damage = damage;
        thisBullet.damageOverTime = damageOverTime;
        thisBullet.damageOverTimeDuration = damageOverTimeDuration;
        thisBullet.slowPercent = slowPercent;
    }
}
