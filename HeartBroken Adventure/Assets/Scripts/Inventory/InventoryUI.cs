using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	private Inventory instance;
	public Transform inventorySlots;
	private SlotScript[] slots;
	public GameObject inventoryUI;

	// Use this for initialization
	void Start () {
		
		instance = Inventory.instance;

		instance.onItemChangedCallback += UpdateUI;

		slots = inventorySlots.GetComponentsInChildren<SlotScript>();

		inventoryUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.I))
			inventoryUI.SetActive (!inventoryUI.activeSelf);
		//if ()

	}

	private void UpdateUI(){
		for (int i = 0; i < slots.Length; i++) {
			if (i < instance.Items.Count) {
				slots [i].AddItem (instance.Items [i]);
			} else {
				slots [i].RemoveFromSlot ();
			}
		}
	}
}
