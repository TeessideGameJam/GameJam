using UnityEngine;
using System.Collections;

public abstract class Powerup : MonoBehaviour
{
    public PowerupEnum powerupType;
    
    protected Transform target = null;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
}
