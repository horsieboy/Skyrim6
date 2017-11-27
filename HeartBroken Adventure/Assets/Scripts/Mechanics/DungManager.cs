using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DungManager : MonoBehaviour {

    #region Variables

    enum Direction
    {
        East, South, West, North
    }

    [Header("Number of Rooms to Generate")]
  	public int Rooms;

	public int GetRooms {get { return Mathf.Abs (Rooms);}}

    [Header("Dungeon Parts Parameters")]
    public int StartingXCoord = 0;
    public int StartingYCoord = 0;
    public int Width = 10;
    public int Height = 10;
    public int CorridorLength = 8;

    [Header("Texture Collections")]
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;
	public GameObject[] PropTiles;
	public GameObject[] EnemySpawns;
	public GameObject PlayerSpawn;
	public GameObject CaveEntrance;

    //Board Stuff
    private Transform DungHolder;
	private int currentRoom;
	private List<Vector2> GroundBoard = new List<Vector2>();
	private List<Vector2> WallBoard = new List<Vector2>();
	private List<Vector2> PropBoard = new List<Vector2> ();

    //Corridor Stuff
    private int CorridorX;
    private int CorridorY;
    private Direction direction;
    private Direction incomingDirection;

    #endregion

	#region VectorDungeon

	void FillVectorRoom(){
	


	}

	void GrandCleaning(){
		TerminateOvelappingTiles (GroundBoard, WallBoard);
		CleanUpArrays (GroundBoard);
		CleanUpArrays (WallBoard);
		CleanUpArrays (PropBoard);
		TerminateOvelappingTiles (WallBoard, PropBoard);
	}

	void CleanUpArrays(List<Vector2> VectorList){
		try{
		for (int i = 0; i < VectorList.Count; i++) {
			for (int j = 0; j < VectorList.Count; j++) {
				if (VectorList [i] == VectorList [j] && i != j)
					VectorList.RemoveAt (i);
				}
			}
		} catch {
			return;
		}
	}

	void TerminateOvelappingTiles(List<Vector2> VectorListOne, List<Vector2> VectorListTwo){
	
		for (int i = 0; i < VectorListOne.Count; i++) {
			for (int j = 0; j < VectorListTwo.Count; j++) {
				if (VectorListOne [i] == VectorListTwo [j])
					VectorListTwo.RemoveAt (j);
			}
		}	
	}

	void InitializeTiles(){
		foreach (var tile in GroundBoard) {
			SpawnObjectAtCoordinates (FloorTiles, tile);
		}
		foreach (var tile in WallBoard) {
			SpawnObjectAtCoordinates (WallTiles, tile);
		}
		//instantiate props all over the map
	}

	void CreateVectorDungeon()
	{
		int VectorXbasis = StartingXCoord;

		int VectorYBasis = StartingYCoord;

		for (int i = 1; i < Width - 2; i++) {
			for (int j = 1; j < Height - 2; j++) {
				PropBoard.Add (new Vector2 (VectorXbasis + i, VectorYBasis + j));
			}
		}

		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1) {
					WallBoard.Add (new Vector2 (VectorXbasis + x, VectorYBasis + y));
				} else {
					GroundBoard.Add (new Vector2 (VectorXbasis + x, VectorYBasis + y));
				}
			}
		}
		if (currentRoom == GetRooms)
			return;
		CreateVectorTunnel (VectorXbasis, VectorYBasis, Width, Height);
	}
				
	void CreateVectorTunnel(int xBasis, int yBasis, int width, int height){
	
		direction = (Direction)Random.Range (0, 4);

		if (direction == incomingDirection)
			direction = (Direction)(((int)direction + 1) % 4);

		switch (direction) {

		case Direction.West:
			CorridorX = xBasis;
			CorridorY = Random.Range (yBasis + 1, yBasis + height - 1);
			for (int i = 0; i < CorridorLength +2; i++) {
				WallBoard.Add (new Vector2 (CorridorX - i, CorridorY + 1));
				GroundBoard.Add (new Vector2 (CorridorX - i, CorridorY));
				WallBoard.Add (new Vector2 (CorridorX - i, CorridorY - 1));
			}
			StartingXCoord = StartingXCoord - CorridorLength - width;
			break;
		case Direction.North:
			CorridorX = Random.Range (xBasis + 1, xBasis + width - 1);
			CorridorY = yBasis + height - 1;
			for (int i = 0; i < CorridorLength + 2; i++) {
				WallBoard.Add (new Vector2 (CorridorX + 1, CorridorY + i));
				GroundBoard.Add (new Vector2 (CorridorX, CorridorY + i));
				WallBoard.Add (new Vector2 (CorridorX - 1, CorridorY + i));
			}
			StartingYCoord = StartingYCoord + CorridorLength + height - 2;
			break;
		case Direction.East:
			CorridorX = xBasis + width - 1;
			CorridorY = Random.Range (yBasis + 1, yBasis + height - 1);
			for (int i = 0; i < CorridorLength + 2; i++) {
				WallBoard.Add (new Vector2 (CorridorX + i, CorridorY + 1));
				GroundBoard.Add (new Vector2 (CorridorX + i, CorridorY));
				WallBoard.Add (new Vector2 (CorridorX + i, CorridorY - 1));
			}
			StartingXCoord = StartingXCoord + CorridorLength + width;
			break;
		case Direction.South:
			CorridorX = Random.Range (xBasis + 1, xBasis + width - 1);
			CorridorY = yBasis;	
			for (int i = 0; i < CorridorLength + 2; i++) {
				WallBoard.Add (new Vector2 (CorridorX + 1, CorridorY - i));
				GroundBoard.Add (new Vector2 (CorridorX, CorridorY - i));
				WallBoard.Add (new Vector2 (CorridorX - 1, CorridorY - i));
			}
			StartingYCoord = StartingYCoord - CorridorLength - height + 2;
			break;
		}

		incomingDirection = (Direction)(((int)direction + 2) % 4);


	}

	#endregion

	#region reusable

    void SpawnObjectAtCoordinates(GameObject[] objects, Vector2 pos)
    {
        GameObject ObjectToSpawn = objects[Random.Range(0, objects.Length)];

        GameObject instance = Instantiate(ObjectToSpawn, pos, Quaternion.identity) as GameObject;

        instance.transform.SetParent(DungHolder);

    }

    #endregion
		
    public void SetupScene()
    {
        DungHolder = new GameObject("Dungeon").transform;
		for (int i = 0; i < GetRooms; i++) {
			currentRoom++;
			CreateVectorDungeon();
		}
		GrandCleaning ();
		InitializeTiles();
        }
    }
