using UnityEngine;
using System.Collections;

public class PowerupUsage : MonoBehaviour
{
    public float usageLeft = 100f;
    public float rateOfDecrementation = 0.2f;

    public PowerupShield mCurrPowerup = null;
    private float mLastDecrease = 0.0f;

    public void SetPowerup(PowerupShield newPowerup)
    {
        mCurrPowerup = newPowerup.GetComponent<PowerupShield>();
    }

    public void ActivatePowerup()
    {
        if (Time.time > rateOfDecrementation + mLastDecrease && mCurrPowerup &&  usageLeft > 0)
        {
            mCurrPowerup.gameObject.SetActive(true);
            usageLeft--;
            mLastDecrease = Time.time;
        }
    }
    
    public void DeactivatePowerup()
    {
        if (mCurrPowerup)
            mCurrPowerup.gameObject.SetActive(false);
    }
}
