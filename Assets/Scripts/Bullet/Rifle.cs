using UnityEngine;
using System.Collections;

public class Rifle : MonoBehaviour
{

    public BulletManager bulletManager;

    public float firerate = 0.2f;

    private float mLastShot = 0.0f;

    public void Shoot()
    {
        if (Time.time < firerate + mLastShot)
            return;

        bulletManager.Shoot(transform.position, transform.rotation);
        mLastShot = Time.time;
    }
}
