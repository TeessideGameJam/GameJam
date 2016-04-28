using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Header("Player Properties")]
    public float speed = 10f;

    [Header("Weapons")]
    public Rifle rifle;
    public PowerupUsage powerupDefense;
    public PowerupUsage powerupOffense;

    private bool mIsShooting = false;

	void Update ()
    {
        float deltaSpeed = speed * Time.deltaTime;

        // Right Analogue (Rotation & shoot)
        float rHorizontal = Input.GetAxis("Horizontal RAS");
        float rVertical = Input.GetAxis("Vertical RAS");
        mIsShooting = rHorizontal != 0 || rVertical != 0; 

        if (mIsShooting)
        {
            // Rotate the player
            float dirAngle = Mathf.Atan2(rHorizontal, rVertical) * Mathf.Rad2Deg;
            Quaternion toRot = Quaternion.Euler(Vector3.forward * -dirAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRot, deltaSpeed);

            // Shoot
            if(rifle)
                rifle.Shoot();

        }

        // Defensive Power
        if (powerupDefense)
        {
            if (Input.GetAxisRaw("Xbox Left Trigger") != 0)
                powerupDefense.Activate();
            else
                powerupDefense.Deactivate();
        }

        // Offensive Power
        if (powerupOffense)
        {
            if (Input.GetAxisRaw("Xbox Right Trigger") != 0)
                powerupOffense.Activate();
            else
                powerupOffense.Deactivate();
        }

        // Left Analogue (Movement)
        transform.Translate(Input.GetAxis("Horizontal LAS") * deltaSpeed, Input.GetAxis("Vertical LAS") * deltaSpeed, 0, Space.World);   
	}

    public bool IsShooting
    {
        get { return mIsShooting; }
    }
}
