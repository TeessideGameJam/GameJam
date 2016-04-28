using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class PowerDefense_FireShield : Powerup
{
    public string enemyTag = "Enemy";
    public float damageRate = 1.0f;

    private float mLastTick = 0.0f;

    private LinkedList<ShapeRender> mEnemies;

    void Awake()
    {
        mEnemies = new LinkedList<ShapeRender>();
    }

    void Start()
    {
        transform.position = anchorPoint.transform.position;
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void OnEnable()
    {   
        mEnemies.Clear();
    }

    void Update()
    {
        if (Time.time > damageRate + mLastTick)
        {
            foreach (ShapeRender shape in mEnemies)
            {
                shape.numberOfEdges--;
                if (shape.numberOfEdges < PolyRender.MIN_VERTICES)
                {
                    shape.gameObject.SetActive(false);
                    mEnemies.Remove(shape);
                    break;
                }

                shape.UpdateShape();
            }
            mLastTick = Time.time;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == enemyTag)
        {
            ShapeRender enemyShape = col.GetComponent<ShapeRender>();
            if (enemyShape)
                mEnemies.AddFirst(enemyShape);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        mEnemies.Remove(col.GetComponent<ShapeRender>());
    }
}