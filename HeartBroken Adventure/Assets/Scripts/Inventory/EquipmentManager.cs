using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

	public static EquipmentManager instance;

	private EquipmentScript[] equipment;

	private Inventory inventoryInstance;

	public delegate void OnEquipmentChanged (EquipmentScript ItemToAdd, EquipmentScript ItemToSwap);
	public OnEquipmentChanged onEquipmentChanged;

	void Awake(){

		instance = this;

		inventoryInstance = Inventory.instance;
	}

	void Start(){
		
		int slots = System.Enum.GetNames (typeof(EquipmentType)).Length;

		equipment = new EquipmentScript[slots];
	}

	public void Equip(EquipmentScript item){
	
		int slotNum = (int)item.type; 

		EquipmentScript itemToSwap = null;

		if (equipment [slotNum] != null) {
			itemToSwap = equipment [slotNum];
			inventoryInstance.CanAddItem (itemToSwap);
		}
		if (onEquipmentChanged != null)
			onEquipmentChanged.Invoke (item, itemToSwap);

		equipment [slotNum] = item;

	}

}
