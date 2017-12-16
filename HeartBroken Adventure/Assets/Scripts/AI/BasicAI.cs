using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAI : PlayerStats {
	
	#region Variables
	private float MinSpeed = 0f;
	private float CruisingSpeed = 2.5f;
	private float CurrentSpeed;
	private float Range = 6f; 
	private float MinRange = 0.1f;// надо отредактировать под модельку персонажа

	private Vector2 PrevPosition;
	private Vector3 playerPos;

	public Canvas ShowHealth;
	public Image Healthbar;

	public PlayerManager playerManager;
	#endregion 

	void Start(){

		playerManager = PlayerManager.instance;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player"))
			Debug.Log ("Rabotaet");
			//Attack (playerManager.player.GetComponent<PlayerStats> ());
			//StartCoroutine( Attack (playerManager.player.GetComponent<PlayerStats> ()));
	}

	void Update(){
		
		CheckForBeingDead (CurrentHealth);
	
		if (Input.GetKeyDown (KeyCode.Y))
			Debug.Log ("PlayerHealth" + playerManager.player.GetComponent<PlayerStats> ().CurrentHealth);
		
		if (Input.GetKeyDown (KeyCode.T))
			TakeDamage (10);


	}

	void FixedUpdate () {
		
		playerPos = GameObject.Find ("Player").transform.position;

		if (Vector2.Distance (transform.position, playerPos) <= Range) {
			if (Vector2.Distance (transform.position, playerPos) < MinRange) {
				CurrentSpeed = MinSpeed;
			}
			else {
				CurrentSpeed = CruisingSpeed;
				transform.position = Vector2.MoveTowards (transform.position, playerPos, CurrentSpeed * Time.fixedDeltaTime);
			}
		} 

		if (transform.position.x - PrevPosition.x < 0 && IsFacingRight)
			Flip ();
		if (transform.position.x - PrevPosition.x > 0 && !IsFacingRight)
			Flip ();

		PrevPosition = transform.position;

	}

	public void Attack(PlayerStats targetStats){
		if (Random.Range (0, 100) <= Agility.GetValue ())
			targetStats.TakeDamage (2 * Damage.GetValue ());
		else {
			float DamageModifier = Random.Range (0, 50) / 100 + 0.75f;
			targetStats.TakeDamage (DamageModifier * Damage.GetValue ());
		}
		//yield return new WaitForSeconds (AttackSpeed.GetValue ());
	}

	public override void TakeDamage(float damage){
		if (Random.Range (0, 100) >= Agility.GetValue()){
			damage -=  Defence.GetValue();
			damage = Mathf.Clamp (damage, 0, int.MaxValue);
			CurrentHealth -= damage;
			Healthbar.fillAmount = CurrentHealth/MaxHealth.GetValue();
			Debug.Log ("Damage has been taken " + damage);
		}
	}



}