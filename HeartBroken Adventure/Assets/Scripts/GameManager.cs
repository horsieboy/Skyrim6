using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public DungManager dungeonscript;

    public static GameManager instance = null;
    // Use this for initialization
    void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        dungeonscript = GetComponent<DungManager>();
        InitializeGame();
    }

    void InitializeGame()
    {

        dungeonscript.SetupScene();
    }

    // Update is called once per frame
    void Update()
    {

    }
}