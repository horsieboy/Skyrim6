using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

	public Item item;

	void OnTriggerEnter(){
		if (gameObject.tag == "Player") {
			PickUp ();
			Debug.Log ("Item picked up");

		} else
			return;
	}

	void PickUp(){
		if (Inventory.instance.CanBePickedUp()) {
			Inventory.instance.items.Add(item);
			Destroy (this.gameObject);
		} else
			return;
	}
}
