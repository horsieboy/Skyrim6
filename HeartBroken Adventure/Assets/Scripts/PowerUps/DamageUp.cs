using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour {

	public float modifier;
	public float TimeToWait = 30f;

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.CompareTag ("Player"))
			StartCoroutine( AddEffect (collider));
	}

	public virtual IEnumerator AddEffect(Collider2D target){

		target.GetComponent<PlayerStats> ().Damage.CurrentValue += modifier;

		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<Collider2D> ().enabled = false;

		yield return new WaitForSeconds (TimeToWait);

		target.GetComponent<PlayerStats>().Damage.CurrentValue -= modifier;

		Destroy (gameObject);

	}
}
