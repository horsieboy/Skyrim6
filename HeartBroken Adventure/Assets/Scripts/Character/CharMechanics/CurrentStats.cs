using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStats : PlayerStats {

	// Use this for initialization
	void Awake () {

		EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}

	public void OnEquipmentChanged (EquipmentScript ItemToAdd, EquipmentScript ItemToRemove)
	{
		if (ItemToAdd != null) {
			Defence.AddModifier (ItemToAdd.defenceModifier);

			Damage.AddModifier (ItemToAdd.damageModifier);

			Agility.AddModifier (ItemToAdd.agilityModifier);
		}

		if (ItemToRemove != null) {
			Defence.RemoveModifier (ItemToRemove.defenceModifier);

			Damage.RemoveModifier (ItemToRemove.damageModifier);

			Agility.RemoveModifier (ItemToRemove.agilityModifier);
		}
	}
}
