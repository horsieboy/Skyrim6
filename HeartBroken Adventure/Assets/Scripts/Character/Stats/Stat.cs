using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

	public float CurrentValue;

	public float StartValue;

	private List<float> Modifiers = new List<float>();

	public float GetValue(){

		CurrentValue = StartValue;
		Modifiers.ForEach (x => CurrentValue += x);
		return CurrentValue;
	}

	public void AddModifier(float mod){
	
		if (mod != 0)
			Modifiers.Add (mod);
	}


	public void RemoveModifier(float mod){

		if (mod != 0)
			Modifiers.Remove (mod);
	}
}
