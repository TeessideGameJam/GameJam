using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

    public float usageLeft = 100f;
    public float rateOfDecrementation = 0.2f;

    private float mLastDecrease = 0.0f;

    public void UsePowerup()
    {
        if (Time.time > rateOfDecrementation + mLastDecrease && usageLeft > 0)
        {
            usageLeft--;
            mLastDecrease = Time.time;
        }
    }
}
