using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class itemPickup : MonoBehaviour
{
    public bool shieldActive = false;	//Used for collision behaviour
    public int pointMultiplier = 1;		//Always multiply score by this. **MAKE SURE IT IS NOT SET TO 0**

    void OnCollisionEnter2D(Collision2D col)
    {
        //Check if there is a collision with a powerup
        if (col.gameObject.tag == "Powerup")
        {
            //If there is, destroy it
            Destroy(col.gameObject);

            //If the powerup is a shield
            if (col.gameObject.name == "Shield")
            {
                //Set shieldActive to true
                shieldActive = true;
            }
        }
    }
}
