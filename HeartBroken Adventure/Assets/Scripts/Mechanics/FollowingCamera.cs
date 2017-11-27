using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

	public Transform target;

	public float SmoothSpeed = 0.125f;

	public Vector3 offset = new Vector3(0,0,-1);
	
	// Update is called once per frame
	void FixedUpdate () {
	
		Vector3 currentPosition = target.position + offset;

		Vector3 smoothedPosition = Vector3.Lerp (transform.position, currentPosition, SmoothSpeed);

		transform.position = smoothedPosition;
	}
}
