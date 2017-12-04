using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AltInput : MonoBehaviour
{
    public EventSystem EventSystem;
    public GameObject SelectedObject;

    private bool ButtonSelected;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetAxisRaw("Vertical") != 0 && ButtonSelected == false)
        {
            EventSystem.SetSelectedGameObject(SelectedObject);
            ButtonSelected = true;
        }
	}

    private void OnDisable()
    {
        ButtonSelected = false;
    }
}
