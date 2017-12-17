using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ExistentialThings {

	public Stat MaxHealth;
	public Stat Damage;
	public Stat Defence;
	public Stat Agility;

	public float CurrentHealth;

	void Awake(){

		this.CurrentHealth = this.MaxHealth.GetValue();
	}

	public virtual void TakeDamage(float damage ){

		if (Random.Range (0, 100) >= this.Agility.GetValue()){
			 damage -=  this.Defence.GetValue();
			damage = Mathf.Clamp (damage, 0, int.MaxValue);
			this.CurrentHealth -= damage;
		}
	}

}
