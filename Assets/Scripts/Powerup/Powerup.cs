using UnityEngine;
using System.Collections;

public abstract class Powerup : MonoBehaviour
{
    public PowerupEnum powerupType;
    
    protected Transform anchorPoint = null;
    protected Transform target = null;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    public void SetAnchor(Transform anchorTransform)
    {
        anchorPoint = anchorTransform;
    }
}
