using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoading : MonoBehaviour {

    public GameObject gameManager;
	void Awake ()
    {

        if (GameManager.instance == null)
            Instantiate(gameManager);    
	}
	
}
