using UnityEngine;
using System.Collections;

public class PowerBullet_BasicBullet : Powerup
{
    void OnEnable()
    {
        if(target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
            gameObject.SetActive(true);
        }
    }
}
