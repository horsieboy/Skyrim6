using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistentialThings : MonoBehaviour {

	public bool IsFacingRight = false;

	public virtual void Flip(){

			IsFacingRight = !IsFacingRight;

			Vector2 direction = transform.localScale;

			direction.x *= -1;

			transform.localScale = direction;

	}
	public virtual void CheckForBeingDead(float health){
		if(health <= 0)
		Destroy (gameObject);
	}

}
