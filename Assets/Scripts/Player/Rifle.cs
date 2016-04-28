using UnityEngine;
using System.Collections;

public class Rifle : Weapon
{
    [Header("Default Values")]
    public PowerupEnum defaultBullet = PowerupEnum.Bullet_normal;
    public float defaultConsumptionRate = 0.2f;

    [Header("Rifle Properties")]
    public PowerupEnum bulletType = PowerupEnum.Bullet_normal;

    public void Shoot()
    {
        if (energy < 0)
            Clear();

        if (!mIsFrozen && Time.time > consumptionRate + mLastDecrease)
        {
            Powerup bullet = poolManager.PullObject(bulletType);
            if (bullet)
            {
                bullet.SetAnchor(anchorPoint);
                bullet.SetTarget(transform);
                bullet.gameObject.SetActive(true);
            }

            energy -= consumptionPerTick;
            mLastDecrease = Time.time;
        }
    }

    public override void Clear()
    {
        bulletType = defaultBullet;
        energy = 1;
        consumptionPerTick = 0;
        consumptionRate = defaultConsumptionRate;
    }

    public override void SetPowerup(PowerupEnum powerupType, float energy, float consumptionPerTick, float consumptionRate)
    {
        bulletType = powerupType;
        this.energy = energy;
        this.consumptionPerTick = consumptionPerTick;
        this.consumptionRate = consumptionRate;
    }
}
