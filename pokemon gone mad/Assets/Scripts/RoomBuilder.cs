using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RoomBuilder : MonoBehaviour
{
    public Biome biome;
    public float noiseSeed;
    private float moisture;
    private float temperature;

    private static readonly int roomWidth = 40;
    private static readonly int roomHeight = 40;
    private Tilemap[] tileMaps;
    private static readonly int gridWidth = roomWidth / 2;
    private static readonly int gridHeight = roomHeight / 2;
    private Vector2 gridCenter = new(gridWidth / 2, gridHeight / 2);

    public enum GridSpace { empty, floor, wall };
    

    // Start is called before the first frame update
    void Start()
    {
        // build a new room here
        GridSpace[,] gridMap = PrepareGrid();
        moisture = GetMoisture(this.transform.position);
        temperature = GetTemperature(this.transform.position);
        Debug.Log("moisture: " + moisture);
        Debug.Log("temperature: " + moisture);
        biome = new(moisture, temperature);
        tileMaps = GetComponentsInChildren<Tilemap>();
        foreach(Tilemap map in tileMaps)
        {
            Debug.Log(map.ToString());
        }
        SpawnLevel(gridMap, biome);


    }

    public GridSpace[,] PrepareGrid()
    {
        GridSpace[,] grid = new GridSpace[gridWidth, gridHeight];
        int lastCol = gridWidth - 1;
        int lastRow = gridHeight - 1;
        int centerCol = (int)gridCenter.x;
        int centerRow = (int)gridCenter.y;

        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                if (row == 0 || col == 0 || row == lastRow || col == lastCol)
                {
                    grid[row, col] = GridSpace.wall;
                }
                else
                {
                    grid[row, col] = GridSpace.empty;
                }

            }
        }

        for (int offset = -2; offset < 2; offset++)
        {
            // top
            grid[centerCol + offset, 0] = GridSpace.floor;
            // bottom
            grid[centerCol + offset, lastRow] = GridSpace.floor;
            // left
            grid[0, centerRow + offset] = GridSpace.floor;
            // right
            grid[lastCol, centerRow + offset] = GridSpace.floor;
        }

        return grid;
    }

    void SpawnLevel(GridSpace[,] grid, Biome biome)
    {
        Tile groundTile = biome.groundTile;
        Tile wallTile = biome.wallTile;
        
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < roomHeight / 2; y++)
            {
                SpawnTile(x, y, groundTile, tileMaps[0]);
                switch (grid[x, y])
                {
                    case GridSpace.empty:
                        break;
                    case GridSpace.floor:
                        break;
                    case GridSpace.wall:
                        SpawnTile(x, y, wallTile, tileMaps[1]);
                        break;
                }   
            }
        }
    }

    void SpawnTile(int x, int y, Tile tile, Tilemap map)
    {
        // Offset the spawn coordinates by half the room size so it is centered properly
        Vector3Int offset = new(gridWidth, gridHeight, 0);

        // spawns the cell and its right, down, and downright neighbors
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector3Int spawnPos = new Vector3Int(x * 2 + i, y * 2 + j, 0) - offset;
                //if (floor)
                //{

                    //float rotationMult = Mathf.Floor(Random.value * 4);
                    //objRot = Quaternion.Euler(0f, 0f, 90f * rotationMult);
                    //Color color = new(1f, 1f, 1f);
                    //Matrix4x4 matrix = Matrix4x4.TRS(spawnPos, objRot, Vector3.one);
                    //TileChangeData tileData = new(spawnPos, tile, color, matrix);
                    map.SetTile(spawnPos, tile);
                    //TileRotationManager trm = map.GetComponent<TileRotationManager>();

                    //trm.RotateTiles(map);

                    //map.GetComponent<TileRotationManager>().RotateTiles();
                //}
            }
        }
    }
    
    float GetMoisture(Vector3 roomOrigin)
    {
        Debug.Log("seed: " + noiseSeed);
        return Mathf.Clamp(Mathf.PerlinNoise(roomOrigin.x * .38f * noiseSeed, roomOrigin.y * .38f * noiseSeed),0,.999999f);
    }

    float GetTemperature(Vector3 roomOrigin)
    {
        Debug.Log("seed: " + noiseSeed);
        return Mathf.Clamp(Mathf.PerlinNoise(roomOrigin.x * .68f * noiseSeed, roomOrigin.y * .68f * noiseSeed), 0, .999999f);
        
    }
}
