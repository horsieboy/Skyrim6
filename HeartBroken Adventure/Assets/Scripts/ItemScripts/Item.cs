using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {

	public Sprite Icon;

	public GameObject gameObj;

	public string Name;

	public int Price;

	public virtual void Use(){
	
	}

	public void RemoveOnInsert(){
		Inventory.instance.RemoveItem (this);
	}
}
