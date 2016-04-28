using UnityEngine;
using System.Collections;

public class PowerOffense_LaserBeam : Powerup {

    public string enemyTag = "Enemy";
    public float damageRate = 0.2f;
    private float mLastTick = 0.0f;

    private ShapeRender mEnemyShape = null;
    private Rifle mPlayerRifle = null;

    void OnEnable()
    {
        if(target)
        {
            transform.position = anchorPoint.position;
            mPlayerRifle = target.GetComponent<Rifle>();
            if(mPlayerRifle)
                mPlayerRifle.IsFrozen = true;
        }
    }

    void OnDisable()
    {
        if(mPlayerRifle)
            mPlayerRifle.IsFrozen = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        mEnemyShape = col.transform.GetComponent<ShapeRender>();
    }

    void OnTriggerStay2D(Collider2D col)
    {   
        if(col.transform.tag == enemyTag)
        {
            if (Time.time > damageRate + mLastTick)
            {
                mEnemyShape.numberOfEdges--;
                if (mEnemyShape.numberOfEdges < PolyRender.MIN_VERTICES)
                    col.gameObject.SetActive(false);

                mEnemyShape.UpdateShape();

                mLastTick = Time.time;
            }            
        }
    }
}
