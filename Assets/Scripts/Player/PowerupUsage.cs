using UnityEngine;
using System.Collections;

public class PowerupUsage : Weapon
{
    public void Activate()
    {
        if (energy < 0)
        {
            Clear();
            return;
        }

        if (!mIsFrozen && Time.time > consumptionRate + mLastDecrease)
        {
            if (mPowerup)
            {
                mPowerup.SetAnchor(anchorPoint);
                mPowerup.SetTarget(transform);

                mPowerup.gameObject.SetActive(true);
            }

            energy -= consumptionPerTick;
            mLastDecrease = Time.time;
        }
    }

    public void Deactivate()
    {
        if(mPowerup)
            mPowerup.gameObject.SetActive(false);
    }

    public override void Clear()
    {
        Deactivate();
        mPowerup = null;
    }

    public override void SetPowerup(PowerupEnum powerupType, float energy, float consumptionPerTick, float consumptionRate)
    {
        if (mPowerup)
            Clear();

        mPowerup = poolManager.PullObject(powerupType);
        this.energy = energy;
        this.consumptionPerTick = consumptionPerTick;
        this.consumptionRate = consumptionRate;
    }

}
