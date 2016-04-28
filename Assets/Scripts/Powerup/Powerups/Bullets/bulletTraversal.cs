using UnityEngine;
using System.Collections;

public class bulletTraversal : MonoBehaviour {

    public float speed = 2.0f;

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(gameObject.activeSelf)
        {
            float deltaSpeed = speed * Time.deltaTime;
            transform.Translate(0, deltaSpeed, 0);

            gameObject.SetActive(renderer.isVisible); // <--- will disable itself if the object is not visible
        }
	}
}
