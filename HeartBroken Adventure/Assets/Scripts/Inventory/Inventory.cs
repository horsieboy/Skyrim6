using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public static Inventory instance;
	public List<Item> Items = new List<Item> ();
	public int InventorySize = 12;
	public delegate void OnItemChanged ();
	public OnItemChanged onItemChangedCallback;
	public int HealingPotionsAmount;
	private float Healing = 50f;
	private CurrentStats playerStats;
	private Text HealingAmountText;

	void Start(){

		playerStats = PlayerManager.instance.player.GetComponent<CurrentStats> ();
	}

	void Awake(){

		instance = this;
	}

	public bool CanAddItem(Item item){
	
		if (Items.Count >= InventorySize)
			return false;

		Items.Add (item);
			if(onItemChangedCallback !=null)			
		onItemChangedCallback.Invoke();

		return true;
	}
		

	public void RemoveItem(Item item)
	{
		if(onItemChangedCallback !=null)
			onItemChangedCallback.Invoke();
		
		Items.Remove (item);
	}

}