using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory instance;

	public delegate void OnInventoryChanged ();

	public OnInventoryChanged onItemChanged;

	public List<Item> items = new List<Item> ();

	public int inventorySize = 20;

	void Awake(){

		//if(instance != null)
		instance = this;
	}

	public bool CanBePickedUp(){
		
		if (items.Count <= inventorySize) 
			return true;
			//Нужно указать ссылку на интерфейс
		else
			return false;
	}
		

	public void RemoveItem(Item item)
	{
		items.Remove (item);
	}
}