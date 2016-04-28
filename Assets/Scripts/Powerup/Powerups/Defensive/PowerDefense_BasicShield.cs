using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShapeRender))]
public class PowerDefense_BasicShield : Powerup {

    [Range(3, 20)]
    public int maxHealth = 4;
    public string enemyTag = "Enemy";

    private ShapeRender mRender = null;
    private PlayerController pc = null;

    void Start()
    {
        mRender = GetComponent<ShapeRender>();
        pc = target.GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == enemyTag)
        {
            mRender.numberOfEdges--;
            if(mRender.numberOfEdges < PolyRender.MIN_VERTICES)
            {
                pc.powerupDefense.Clear();
                gameObject.SetActive(false);
                mRender.numberOfEdges = maxHealth;
            }
            mRender.UpdateShape();
        }
    }
}
