using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ExistentialThings {

	public Stat MaxHealth;
	public Stat Damage;
	public Stat Defence;
	public Stat MaxSpeed;
	public Stat Agility;
	public Stat Level;
	public Stat AttackSpeed;

	public float CurrentHealth;

	void Awake(){

		CurrentHealth = MaxHealth.GetValue();

	}

	public virtual void TakeDamage(float damage ){

		if (Random.Range (0, 100) >= Agility.GetValue()){
			 damage -=  Defence.GetValue();
			damage = Mathf.Clamp (damage, 0, int.MaxValue);
			CurrentHealth -= damage;
		}
	}

}
