using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungManager : MonoBehaviour {

    #region Variables

    public class Count
    {

        public int MinValue;
        public int MaxValue;

        public Count(int min, int max)
        {
            MinValue = min;

            MaxValue = max;
        }

    }

    enum Direction
    {
        East, South, West, North
    }

    [Header("Number of Rooms to Generate")]
    public int Rooms;

    [Header("Dungeon Parts Parameters")]
    public int StartingXCoord = 0;
    public int StartingYCoord = 0;
    public int Width = 10;
    public int Height = 10;
    public int CorridorLength = 8;

    [Header("Texture Collections")]
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;

    //Board Stuff
    private Transform DungHolder;
    private List<Vector2> gridPositions = new List<Vector2>();
    private int CurrentRoom = 0;

    //Corridor Stuff
    private int CorridorX;
    private int CorridorY;
    private Direction direction;
    private Direction incomingDirection;
    // Even needed?

    #endregion

    void InitializeList()
    {

        gridPositions.Clear();

        for (int i = 1; i < Width; i++)
        {
            for (int j = 1; j < Height; j++)
            {
                gridPositions.Add(new Vector2(i, j));
            }
        }

    }

    void SpawnObjectAtCoordinates(GameObject[] objects, int x, int y)
    {
        GameObject ObjectToSpawn = objects[Random.Range(0, objects.Length)];

        GameObject instance = Instantiate(ObjectToSpawn, new Vector2(x , y), Quaternion.identity) as GameObject;

        instance.transform.SetParent(DungHolder);

    }

    void CreateCorridor(int xBasis, int yBasis, int width , int height )
    {

        direction = (Direction)Random.Range(0, 4);

        if (direction == incomingDirection)
        {
            int directionIndex = (int)direction;
            directionIndex = (directionIndex + 1) % 4;
            direction = (Direction)directionIndex;
        }

        switch (direction)
        {
            case Direction.West:
                CorridorX = xBasis;
                CorridorY = Random.Range(yBasis + 1, yBasis + height - 1);
                for (int i = 0; i < CorridorLength; i++)
                {
                    SpawnObjectAtCoordinates(WallTiles, CorridorX - i, CorridorY + 1);

                    SpawnObjectAtCoordinates(FloorTiles, CorridorX - i, CorridorY);

                    SpawnObjectAtCoordinates(WallTiles, CorridorX - i, CorridorY - 1);

                }
                SpawnObjectAtCoordinates(WallTiles, StartingXCoord, StartingYCoord + 1);
                StartingXCoord = StartingXCoord - CorridorLength - width + 2;

                break;
                
            case Direction.North:
                CorridorX = Random.Range(xBasis + 1, xBasis + width - 1);
                CorridorY = yBasis + height - 1;
                for (int i = 0; i < CorridorLength; i++)
                {
                    SpawnObjectAtCoordinates(WallTiles, CorridorX + 1, CorridorY + i);

                    SpawnObjectAtCoordinates(FloorTiles, CorridorX, CorridorY + i);

                    SpawnObjectAtCoordinates(WallTiles, CorridorX - 1, CorridorY + i);

                }
                StartingYCoord = StartingYCoord + CorridorLength + height - 2;
                break;

            case Direction.South:
                CorridorX = Random.Range(xBasis + 1, xBasis + width - 1) ;
                CorridorY = yBasis;
                for (int i = 0; i < CorridorLength; i++)
                {
                    SpawnObjectAtCoordinates(WallTiles, CorridorX + 1, CorridorY - i);

                    SpawnObjectAtCoordinates(FloorTiles, CorridorX, CorridorY - i);

                    SpawnObjectAtCoordinates(WallTiles, CorridorX - 1, CorridorY - i);

                }
                StartingYCoord = StartingYCoord - CorridorLength - height + 2;
                break;

            case Direction.East:
                CorridorX = xBasis + width - 1 ;
                CorridorY = Random.Range(yBasis + 1, yBasis + height - 1);
                for (int i = 0; i < CorridorLength; i++)
                {
                    SpawnObjectAtCoordinates(WallTiles, CorridorX + i, CorridorY + 1);

                    SpawnObjectAtCoordinates(FloorTiles, CorridorX + i, CorridorY);

                    SpawnObjectAtCoordinates(WallTiles, CorridorX + i, CorridorY - 1);

                }
                StartingXCoord = StartingXCoord + CorridorLength + width - 2;
                break;

        }
                incomingDirection = (Direction)(((int)direction + 2) % 4);
        
    }

    void DungeonRoomCreate()
    {


        int xBasis = StartingXCoord;

        int yBasis = StartingYCoord;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject ObjectToSpawn = FloorTiles[Random.Range(0, FloorTiles.Length)];
                if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    ObjectToSpawn = WallTiles[Random.Range(0, WallTiles.Length)];

                GameObject instance = Instantiate(ObjectToSpawn, new Vector2(xBasis + x, yBasis + y), Quaternion.identity) as GameObject;

                instance.transform.SetParent(DungHolder);
            }
        }

        if (CurrentRoom == Rooms)
            return;
        CreateCorridor(xBasis, yBasis, Width, Height);

    }

    #region forgetIt
    Vector2 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector2 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

   void SpawnRandomObject(GameObject[] tileArray, int min, int max)
    {

        int ObjectCount = Random.Range(min, max + 1);

        for (int i = 0; i < ObjectCount; i++)
        {
            Vector2 randomPosition = RandomPosition();
            GameObject tile = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tile, randomPosition, Quaternion.identity);

        }
    }
#endregion

    public void SetupScene()
    {
        DungHolder = new GameObject("Dungeon").transform;
        for (int i = 0; i < Rooms; i++)
        {
            CurrentRoom++;
        DungeonRoomCreate();
        }


        InitializeList();
    }

}
