using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

	public Vector2 playerPos;

	void Start () {

		GameObject.Find ("Player").transform.position = transform.position;

	}
}
