using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    public Vector3 center;
    //public Biome biome;
    private float moisture;
    private float temperature;
    private int roomWidth = 40;
    private int roomHeight = 40;
    private Object roomContainer = Resources.Load("Prefabs/RoomContainer");

    enum GridSpace { empty, floor, wall };
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildRoom(Vector3 roomOrigin)
    {
        center = roomOrigin;
        Instantiate(roomContainer, center, Quaternion.identity);
        // creates a grid half the width/height of the final room so that forms are all 2x bigger for more space
        GridSpace[,] grid = new GridSpace[20, 20];

        // initialize every cell as empty
        for(int x=0; x < roomWidth/2 - 1; x++)
        {
            for(int y=0; y < roomHeight/2 - 1; y++)
            {
                grid[x, y] = GridSpace.empty;
            }
        }

        SpawnLevel(grid);
        
    }

    void SpawnLevel(GridSpace[,] grid)
    {
        for (int x = 0; x < roomWidth / 2 - 1; x++)
        {
            for (int y = 0; y < roomHeight / 2 - 1; y++)
            {
                SpawnTile(x, y, true);
            }
        }
    }

    void SpawnTile(int x, int y, bool floor)
    {
        Vector2 offset = new(roomWidth / 2, roomHeight / 2);
        Quaternion objRot;

        // spawns the cell and its right, down, and downright neighbors
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector2 spawnPos = new Vector2(x * 2 + i, y * 2 + j) - offset;
                if (floor)
                {
                    float rotationMult = Mathf.Floor(Random.value * 4);
                    objRot = Quaternion.Euler(0f, 0f, 90f * rotationMult);
                }
            }
        }
    }
    
    float GetMoisture(Vector3 roomOrigin)
    {
        return 1f;
    }

    float GetTemperature(Vector3 roomOrigin)
    {
        return 1f;
    }
}
