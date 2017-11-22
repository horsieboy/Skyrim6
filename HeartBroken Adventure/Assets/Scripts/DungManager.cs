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

    private Direction direction;

    public int StartingXCoord = 0;
    public int StartingYCoord = 0;
    public int Width = 10;
    public int Height = 10;
    public int CorridorLength = 8;

    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;

    private Transform DungHolder;
    private List<Vector2> gridPositions = new List<Vector2>();
    private int CorridorX = 0;
    private int CorridorY = 0;
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

    // out int xEnd, int yEnd
    void CreateCorridorEntrancePoint(int xBasis, int yBasis, int width , int height )
    {

        direction = Direction.West; //
            //(Direction)Random.Range(0, 4);

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
                CorridorX -= CorridorLength;
                //Проверка на существование координаты
                SpawnObjectAtCoordinates(WallTiles, CorridorX + 1, CorridorY);
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
                CorridorY += CorridorLength;
                break;

            case Direction.South:
                CorridorX = Random.Range(xBasis + 1, xBasis + width - 1);
                CorridorY = yBasis;
                for (int i = 0; i < CorridorLength; i++)
                {
                    SpawnObjectAtCoordinates(WallTiles, CorridorX + 1, CorridorY - i);

                    SpawnObjectAtCoordinates(FloorTiles, CorridorX, CorridorY - i);

                    SpawnObjectAtCoordinates(WallTiles, CorridorX - 1, CorridorY - i);

                }
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
                break;

        }
        
    }

    void DungeonRoomCreate()
    {

        DungHolder = new GameObject("Dungeon").transform;

        int xBasis = StartingXCoord;
        int yBasis = StartingYCoord;

        for (int x = xBasis; x < Width; x++)
        {
            for (int y = yBasis; y < Height; y++)
            {
                GameObject ObjectToSpawn = FloorTiles[Random.Range(0, FloorTiles.Length)];
                if (x == xBasis || x == Width - 1|| y == yBasis || y == Height - 1)
                    ObjectToSpawn = WallTiles[Random.Range(0, WallTiles.Length)];
                
                GameObject instance = Instantiate(ObjectToSpawn, new Vector2(StartingXCoord + x, StartingYCoord + y), Quaternion.identity) as GameObject;

                instance.transform.SetParent(DungHolder);

            }
        }

        CreateCorridorEntrancePoint(xBasis, yBasis, Width, Height);

        
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

        DungeonRoomCreate();
        InitializeList();
    }

}
