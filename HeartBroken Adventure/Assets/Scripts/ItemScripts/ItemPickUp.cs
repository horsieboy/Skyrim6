using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {

	public Item item;

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.CompareTag ("Player")) {
			bool PickedUp =  Inventory.instance.CanAddItem (item);
			if(PickedUp)
			Destroy (gameObject);
		}
	}
}

