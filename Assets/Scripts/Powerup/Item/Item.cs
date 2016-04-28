using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Item : MonoBehaviour {

    public PowerupEnum powerup;
    public ItemType itemType;

    public float energy;
    public float consumptionPerTick;
    public float consumptionRate;

    void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player")
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if(player)
            {
                Weapon playerWeapon = null;

                switch(itemType)
                {
                    case ItemType.defensive:
                        playerWeapon = player.powerupDefense;
                        break;
                    case ItemType.offensive:
                        playerWeapon = player.powerupOffense;
                        break;
                    default:
                        playerWeapon = player.rifle;
                        break;
                }

                playerWeapon.SetPowerup(powerup, energy, consumptionPerTick, consumptionRate);
                gameObject.SetActive(false);
            }
        }
    }

}
