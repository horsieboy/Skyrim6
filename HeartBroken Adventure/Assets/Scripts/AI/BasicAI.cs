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
	public float AttackSpeed = 1.5f;
	private Vector2 PrevPosition;
	private Vector3 playerPos;
	public List<GameObject> ObjectsToDrop;
	public Canvas ShowHealth;
	public Image Healthbar;

	public CurrentStats playerStats;
	#endregion 

	void Start(){

		playerStats = PlayerManager.instance.player.GetComponent<CurrentStats>();

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player"))
			Debug.Log ("Damage has been dealt");
			//StartCoroutine (Attack (PlayerManager.instance.player.GetComponent<CurrentStats>()));
	}

	void Update(){
		
		CheckForBeingDead (CurrentHealth);

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

	public IEnumerator Attack(PlayerStats targetStats){
		if (Random.Range (0, 100) <= this.Agility.GetValue ())
			targetStats.TakeDamage (2 * Damage.GetValue ());
		else {
			float DamageModifier = Random.Range (0, 50) / 100 + 0.75f;
			targetStats.TakeDamage (DamageModifier * 10f );
		}
		yield return new WaitForSeconds (AttackSpeed);
	}

	public override void TakeDamage(float damage){
		if (Random.Range (0, 100) >= Agility.GetValue()){
			damage -= Defence.GetValue();
			damage = Mathf.Clamp (damage, 0, int.MaxValue);
			CurrentHealth -= damage;
			Healthbar.fillAmount = CurrentHealth/MaxHealth.GetValue();
			Debug.Log ("Damage has been taken " + damage);
		}
	}


	public override void CheckForBeingDead(float health){
		if (health <= 0) {
			if (Random.Range (0, 100) <= 15) {
				GameObject drop = ObjectsToDrop [Random.Range (0, ObjectsToDrop.Count)];
				GameObject instance = Instantiate (drop, transform.position, Quaternion.identity) as GameObject;
				instance.transform.SetPositionAndRotation (transform.position, Quaternion.identity);
			}

			Destroy (gameObject);
		}
		
	}
}