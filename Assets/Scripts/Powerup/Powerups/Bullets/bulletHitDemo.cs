using UnityEngine;
using System.Collections;

public class bulletHitDemo : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Enemy")
        {
            ShapeRender enemyShape = col.transform.GetComponent<ShapeRender>();
            enemyShape.numberOfEdges--;

            if (enemyShape.numberOfEdges < PolyRender.MIN_VERTICES)
                col.gameObject.SetActive(false);
            
            enemyShape.UpdateShape();
            gameObject.SetActive(false);
        }
    }

}
