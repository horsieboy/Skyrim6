using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType{
	
	Weapon, Head, Body, Legs, Accessory
}
	
[CreateAssetMenu]
public class EquipmentScript : Item {

	public EquipmentType type;

	public int defenceModifier;

	public int damageModifier;

	public int agilityModifier;

	public override void Use (){
		base.Use ();

		EquipmentManager.instance.Equip (this);

		RemoveOnInsert ();
	}
}