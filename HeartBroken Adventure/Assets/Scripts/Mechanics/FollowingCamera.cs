using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

	private Vector3 playerPosition;

	public float SmoothSpeed = 0.125f;

	public Vector3 offset = new Vector3(0,0,-1);

	// Update is called once per frame
	void FixedUpdate () {

		playerPosition = GameObject.Find ("Player").transform.position;

		Vector3 currentPosition = playerPosition + offset;

		Vector3 smoothedPosition = Vector3.Lerp (transform.position, currentPosition, SmoothSpeed);

		transform.position = smoothedPosition;
	}
}
