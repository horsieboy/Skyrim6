using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LevelExit : MonoBehaviour {

	private float timeToWait = 5f;

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.CompareTag ("Player"))
			StartCoroutine (BeginExiting());
		}

	private IEnumerator BeginExiting(){
		
		yield return new WaitForSeconds (timeToWait);
		if (Mathf.Abs ((GameObject.Find ("Player").transform.position - transform.position).magnitude) < 0.75f)
			Restart ();
	}

	private void Restart(){

		Application.LoadLevel (Application.loadedLevel);
	}
}
