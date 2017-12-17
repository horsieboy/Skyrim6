using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour {


	private Item item;
	public Image icon;
	public Button removeButton;

	public void AddItem(Item itemToAdd){
		
		item = itemToAdd;

		icon.sprite = item.Icon;

		icon.enabled = true;

		removeButton.interactable = true;

	}

	public void RemoveFromSlot(){
	
		item = null;

		icon.sprite =null;

		icon.enabled = false;

		removeButton.interactable = false;
	}

	public void RemoveClick(){
		Inventory.instance.RemoveItem (item);
	}

	public void UseItem(){
	
		if (item != null) {
			item.Use ();
		}
	
	}
}
