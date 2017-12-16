using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ExistentialThings {// MonoBehaviour {


	#region Speed Setting
	private float MaxSpeed = 3.5f;
	private float CurrentSpeed = 0;
	private float MinSpeed = 0;
	private float Acceleration = 0.1f;
	private PlayerStats stats;
	#endregion

	void Start(){
		stats  = PlayerManager.instance.player.GetComponent<PlayerStats>();
	}


	void FixedUpdate () {

		CheckForBeingDead (stats.CurrentHealth);

		float SpeedX = Input.GetAxis ("Horizontal");

		float SpeedY = Input.GetAxis ("Vertical");

		if (SpeedX == 0 && SpeedY == 0)
			CurrentSpeed = MinSpeed;
		
		if (CurrentSpeed < MaxSpeed)
			CurrentSpeed += Acceleration;

		transform.Translate (new Vector2 (SpeedX, SpeedY) * CurrentSpeed * Time.fixedDeltaTime);

		if (SpeedX > 0 && !IsFacingRight)
			Flip ();
		else if (SpeedX < 0 && IsFacingRight)
			Flip ();

	}



}
