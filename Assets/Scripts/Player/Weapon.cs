using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

    public PoolManager poolManager;

    [Header("Powerup Usage Properties")]
    public float energy;
    public float consumptionPerTick;
    public float consumptionRate;

    protected float mLastDecrease = 0.0f;
    protected Powerup mPowerup = null;
    protected bool mIsFrozen = false;

    public abstract void SetPowerup(PowerupEnum powerupType, float energy, float consumptionPerTick, float consumptionRate);
    public abstract void Clear();

    public bool IsFrozen
    {
        get { return mIsFrozen; }
        set { mIsFrozen = value; }
    }
    
}
