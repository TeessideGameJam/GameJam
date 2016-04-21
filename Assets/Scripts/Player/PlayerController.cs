using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;

    private Rifle mPlayerRifle = null;
    public Powerup powerDefensive = null;
    public Powerup powerOffensive = null;

    void Start()
    {
        mPlayerRifle = GetComponent<Rifle>();
    }

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
            if(mPlayerRifle)
                mPlayerRifle.Shoot();
        }

        if(Input.GetAxis("Xbox Left Trigger") != 0)
        {
            if (powerDefensive)
                powerDefensive.UsePowerup();
        }

        if(Input.GetAxis("Xbox Right Trigger") !=0)
        {
            if (powerOffensive)
                powerOffensive.UsePowerup();
        }



        // Left Analogue (Movement)
        transform.Translate(Input.GetAxis("Horizontal LAS") * deltaSpeed, Input.GetAxis("Vertical LAS") * deltaSpeed, 0, Space.World);   
	}

    public bool IsShooting
    {
        get { return mIsShooting; }
    }
}
