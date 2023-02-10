using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RoomBuilder : MonoBehaviour
{
    public Biome biome;
    public float[] noiseSeedMoisture = new float[2];
    public float[] noiseSeedTemperature = new float[2];
    private float scale = 2f;
    private int moisture;
    private int temperature;

    private static readonly int roomWidth = 40;
    private static readonly int roomHeight = 40;
    private Tilemap[] tileMaps;
    private static readonly int gridWidth = roomWidth / 2;
    private static readonly int gridHeight = roomHeight / 2;
    private Vector2 gridCenter = new(gridWidth / 2, gridHeight / 2);

    public enum GridSpace { empty, floor, wall };


    private void Awake()
    {
        //noiseSeedMoisture[0] = Random.value;
        //noiseSeedMoisture[1] = Random.value;
        //noiseSeedTemperature[0] = Random.value;
        //noiseSeedTemperature[1] = Random.value;
        noiseSeedMoisture[0] = .3f;
        noiseSeedMoisture[1] = .5f;
        noiseSeedTemperature[0] = .7f;
        noiseSeedTemperature[1] = .4f;
    }
    // Start is called before the first frame update
    void Start()
    {
        // build a new room here
        GridSpace[,] gridMap = PrepareGrid();

        moisture = GetMoisture(this.transform.position);
        temperature = GetTemperature(this.transform.position);
        Debug.Log("moisture: " + moisture);
        Debug.Log("temperature: " + moisture);
        biome = Biome.NewBiome(moisture, temperature);
        tileMaps = GetComponentsInChildren<Tilemap>(); // 0: Water, 1: Ground, 2: Walls/Objects, 3: DecorativeObjects

        SpawnLevel(gridMap);


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

    void SpawnLevel(GridSpace[,] grid)
    {
        Tile groundTile = biome.groundTile;
        Tile wallTile = biome.wallTile;
        
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < roomHeight / 2; y++)
            {
                if(biome.GetType().ToString().StartsWith("Water"))
                {
                    SpawnTile(x, y, groundTile, tileMaps[0]);
                }
                else
                {
                    SpawnTile(x, y, groundTile, tileMaps[1]);
                }
                switch (grid[x, y])
                {
                    case GridSpace.empty:
                        break;
                    case GridSpace.floor:
                        break;
                    case GridSpace.wall:
                        SpawnTile(x, y, wallTile, tileMaps[2]);
                        if (biome.wallTileCount > 1) SpawnTile(x, y, biome.wallTile2, tileMaps[3], true);
                        break;
                }   
            }
        }
    }

    void SpawnTile(int x, int y, Tile tile, Tilemap map, bool twoTileWall = false)
    {
        // Offset the spawn coordinates by half the room size so it is centered properly
        Vector3Int offset = new(gridWidth, gridHeight, 0);

        // spawns the cell and its right, up, and up-right neighbors
        for (int i = 0; i < 2; i++)
        {
            // If this is the top row, don't set the upper tiles for the two tile wall
            if(twoTileWall && y == gridHeight - 1)
            {
                Vector3Int spawnPos = new Vector3Int(x * 2 + i, y * 2+1, 0) - offset;
                map.SetTile(spawnPos, tile);
                continue;
            }
            for (int j = 0; j < 2; j++)
            {
                Vector3Int spawnPos = new Vector3Int(x * 2 + i, y * 2 + j, 0) - offset;
                if (twoTileWall) spawnPos.y++;
                map.SetTile(spawnPos, tile);
            }
        }
    }
    
    int GetMoisture(Vector3 roomOrigin)
    {
        float perlinRaw = Mathf.PerlinNoise((noiseSeedMoisture[1] * roomOrigin.x/roomWidth + noiseSeedMoisture[0]) / scale, ( noiseSeedMoisture[1]) / scale);
        
        float perlinClamp = Mathf.Clamp(perlinRaw, 0, .9999999f);
        Debug.Log("Moisture perlinRaw: " + perlinRaw +" perlinClamp: " + perlinClamp);
        return Mathf.FloorToInt(perlinClamp * 100);
    }

    int GetTemperature(Vector3 roomOrigin)
    {
        float perlinRaw = Mathf.PerlinNoise((noiseSeedTemperature[1] * roomOrigin.x/roomWidth + noiseSeedTemperature[0]) / scale, (noiseSeedTemperature[0] * roomOrigin.y/roomHeight + noiseSeedTemperature[1]) / scale);
        float perlinClamp = Mathf.Clamp(perlinRaw, 0, .9999999f);
        Debug.Log("Temperature perlinRaw: " + perlinRaw + " perlinClamp: " + perlinClamp);
        return Mathf.FloorToInt(perlinClamp * 100);

    }
}
