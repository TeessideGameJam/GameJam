using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShapeRender))]
public class PowerupShield : MonoBehaviour {

    private ShapeRender render = null;

    void Start()
    {
        render = GetComponent<ShapeRender>();
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Enemy")
        {
            render.numberOfEdges--;
            if(render.numberOfEdges < PolyRender.MIN_VERTICES)
            {
                gameObject.SetActive(false);
                return;
            }

            render.UpdateShape();
        }
    }

}
