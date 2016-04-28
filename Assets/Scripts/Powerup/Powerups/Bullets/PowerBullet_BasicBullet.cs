using UnityEngine;
using System.Collections;

public class PowerBullet_BasicBullet : Powerup
{
    void OnEnable()
    {
        if(target)
        {
            transform.position = anchorPoint.position;
            transform.rotation = anchorPoint.rotation;
            gameObject.SetActive(true);
        }
    }
}
