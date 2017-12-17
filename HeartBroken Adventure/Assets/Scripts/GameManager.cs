using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public float levelDelay = 3f;
	public DungManager dungeonscript;
    public static GameManager instance = null;
	private int level = 1;
	private Text levelNum;
	private GameObject levelImage;
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

	void OnLevelWasLoaded(int index){
		level++;
		InitializeGame ();
	}

	private void HideUI(){
		levelImage.SetActive (false);
	}

    void InitializeGame(){
		levelImage = GameObject.Find ("LevelImage");
		levelNum = GameObject.Find ("Level").GetComponent<Text>();
		levelNum.text = "Level " + level;
		levelImage.SetActive (true);
		Invoke ("HideUI", levelDelay);
		dungeonscript.SetupScene(level);
		
    }

	public void GameOver(){
		levelImage.SetActive (true);
		levelNum.text = "Your struggle ends on " + level + " level";
	}
}