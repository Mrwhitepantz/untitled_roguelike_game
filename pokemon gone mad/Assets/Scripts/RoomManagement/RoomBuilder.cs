using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RoomBuilder : MonoBehaviour
{
    // Biome Generation Variables
    public Biome biome;
    private readonly float perlinScale = 3f;    // determines how quickly the values change from room to room
    private int humidity;
    private int temperature;

    // Grid Generation Variables
    private static readonly int roomWidth = 40;
    private static readonly int roomHeight = 40;
    private static readonly int gridWidth = roomWidth / 2;
    private static readonly int gridHeight = roomHeight / 2;
    private Vector2 gridCenter = new(gridWidth / 2, gridHeight / 2);
    private Vector3 roomOrigin;

    private Tilemap[] tileMapsArray;

    public enum GridSpaceType { empty, floor, wall };

    public void BuildRoom(float[] noiseSeedArray, Grid grid)
    {
        // build a new room here
        GridSpaceType[,] gridMap = PrepareGrid();
        roomOrigin = this.transform.position;

        humidity = GetHumidity(new float[] { noiseSeedArray[0], noiseSeedArray[1] });
        temperature = GetTemperature(new float[] { noiseSeedArray[2], noiseSeedArray[3] });
        biome = Biome.NewBiome(humidity, temperature);
        tileMapsArray = grid.GetComponentsInChildren<Tilemap>(); // 0: Water, 1: Ground, 2: EnvironmentObjects, 3: EnvironmentDecorations


        SpawnLevel(gridMap);
    }

    public GridSpaceType[,] PrepareGrid()
    {
        GridSpaceType[,] grid = new GridSpaceType[gridWidth, gridHeight];
        int lastCol = gridWidth - 1;
        int lastRow = gridHeight - 1;
        int centerCol = (int)gridCenter.x;
        int centerRow = (int)gridCenter.y;

        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                // make the edges of the grid into walls so each room is self-contained
                if (row == 0 || col == 0 || row == lastRow || col == lastCol)
                {
                    grid[row, col] = GridSpaceType.wall;
                }
                else
                {
                    grid[row, col] = GridSpaceType.empty;
                }

            }
        }

        // set exits in centers of each side of the room to floor
        for (int offset = -2; offset < 2; offset++)
        {
            // top
            grid[centerCol + offset, 0] = GridSpaceType.floor;
            // bottom
            grid[centerCol + offset, lastRow] = GridSpaceType.floor;
            // left
            grid[0, centerRow + offset] = GridSpaceType.floor;
            // right
            grid[lastCol, centerRow + offset] = GridSpaceType.floor;
        }

        return grid;
    }

    void SpawnLevel(GridSpaceType[,] grid)
    {
        TileBase groundTile = biome.groundTile;
        TileBase wallTile = biome.wallTile;
        bool water = biome.GetType().ToString().StartsWith("Water");

        for (int col = 0; col < gridWidth; col++)
        {
            for (int row = 0; row < roomHeight / 2; row++)
            {
                switch (grid[col, row])
                {
                    case GridSpaceType.empty:
                        if (water)
                        {
                            SpawnTile(col, row, wallTile, tileMapsArray[0]);
                        }
                        else SpawnTile(col, row, groundTile, tileMapsArray[1]);
                        break;
                    case GridSpaceType.floor:
                        SpawnTile(col, row, groundTile, tileMapsArray[1]);
                        break;
                    case GridSpaceType.wall:
                        if (!water)
                        {
                            SpawnTile(col, row, groundTile, tileMapsArray[1]);
                            SpawnTile(col, row, wallTile, tileMapsArray[2]);
                            if (biome.wallTileCount > 1) SpawnTile(col, row, biome.wallTile2, tileMapsArray[3], true);
                        }
                        else SpawnTile(col, row, wallTile, tileMapsArray[0]);
                        
                        break;
                }   
            }
        }
    }

    void SpawnTile(int col, int row, TileBase tile, Tilemap map, bool twoTileWall = false)
    {
        // Offset the spawn coordinates by half the room size so it is centered properly
        // and subtract the roomOrigin to spawn in correct place
        Vector3Int offset = new(gridWidth, gridHeight, 0);
        offset.x -= (int)roomOrigin.x;
        offset.y -= (int)roomOrigin.y;

        // spawns the cell and its right, top, and top-right neighbors
        // this ensures all paths are at least two cells wide to make movement easier
        for (int i = 0; i < 2; i++)
        {
            // If this is the top row, don't set the upper tiles for the two tile wall
            if(twoTileWall && row == gridHeight - 1)
            {
                Vector3Int spawnPos = new Vector3Int(col * 2 + i, row * 2+1, 0) - offset;
                map.SetTile(spawnPos, tile);
                continue;
            }
            for (int j = 0; j < 2; j++)
            {
                Vector3Int spawnPos = new Vector3Int(col * 2 + i, row * 2 + j, 0) - offset;
                if (twoTileWall) spawnPos.y++;
                map.SetTile(spawnPos, tile);
            }
        }
    }
    
    int GetHumidity(float[] noiseSeeds)
    {
        float noiseX = noiseSeeds[1] * roomOrigin.x / roomWidth;
        float noiseY = noiseSeeds[0] * roomOrigin.y / roomHeight;
        
        float perlinRaw = Mathf.PerlinNoise((noiseX + noiseSeeds[0]) / perlinScale, (noiseY + noiseSeeds[1]) / perlinScale); 
        // PerlinNoise can return values slightly below 0 or above 1,
        // so needs to be clamped to ensure mapping works correctly
        float perlinClamp = Mathf.Clamp(perlinRaw, 0, .9999999f);
        return Mathf.FloorToInt(perlinClamp * 100);
    }

    int GetTemperature(float[] noiseSeeds)
    {
        float noiseX = noiseSeeds[1] * roomOrigin.x / roomWidth;
        float noiseY = noiseSeeds[0] * roomOrigin.y / roomHeight;

        float perlinRaw = Mathf.PerlinNoise(( noiseX + noiseSeeds[0]) / perlinScale, (noiseY + noiseSeeds[1]) / perlinScale);
        // PerlinNoise can return values slightly below 0 or above 1,
        // so needs to be clamped to ensure mapping works correctly
        float perlinClamp = Mathf.Clamp(perlinRaw, 0, .9999999f);
        return Mathf.FloorToInt(perlinClamp * 100);
    }
}
