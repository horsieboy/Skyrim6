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
		//необходимо переписать на открытие меню
        dungeonscript.SetupScene();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
//Камера вроде готова
//Генерация готова на 75%
//Инвентарь и предемты не готовы вовсе
//ИИ готов на 55%
//Компоновка находится под вопросом
//Персонаж тоже говот, кстати

//Чтобы доделать генерацию необходимо сделать пикапы, ии, локации и сундуки