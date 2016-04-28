using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

	public Transform player;
	public float distance = 25f;

	// Update is called once per frame
	void Update()
	{
		transform.position = player.position - (Vector3.forward * distance);
	}
}
