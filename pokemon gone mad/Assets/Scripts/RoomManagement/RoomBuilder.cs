using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;


public class RoomBuilder : MonoBehaviour
{
    // Biome Generation Variables
    public Biome biome;
    private readonly float perlinScale = 3f;    // determines how quickly the values change from room to room
    private int humidity;
    private int temperature;

    // Grid Generation Variables
    private int inty;
    private static readonly int roomWidth = 40;
    private static readonly int roomHeight = 40;
    private static readonly int gridWidth = roomWidth / 2;
    private static readonly int gridHeight = roomHeight / 2;
    private static readonly int exitOffset = 2;
    private Vector2Int gridCenter = new(gridWidth / 2, gridHeight / 2);
    private Vector3 roomOrigin;
    private GameObject spawn;
    private GameObject charmander;
    private int mobCap=5;
    private int mobcount=0;

    private Tilemap[] tileMapsArray;

    public enum GridSpaceType { empty, floor, wall };
    public void Update()
    {
        //spawn = GameObject.FindGameObjectWithTag("testspawn");
    }

    public void BuildRoom(float[] noiseSeedArray, Grid grid, GameObject prefab,GameObject prefab2, int flip)
    {
        // build a new room here
        spawn = prefab;
        charmander = prefab2;
        inty = flip;
        GridSpaceType[,] gridMap = PrepareGrid(gridWidth, gridHeight);
        roomOrigin = this.transform.position;

        humidity = GetHumidity(new float[] { noiseSeedArray[0], noiseSeedArray[1] });
        temperature = GetTemperature(new float[] { noiseSeedArray[2], noiseSeedArray[3] });
        biome = Biome.NewBiome(humidity, temperature);
        tileMapsArray = grid.GetComponentsInChildren<Tilemap>(); // 0: Water, 1: Ground, 2: EnvironmentObjects, 3: EnvironmentDecorations

        if (biome is ForestBiome)
        {
            MazeGenerator mazeGen = new(gridMap);
            mazeGen.CreateClearings(gridWidth, gridHeight, 3, 12, true);
            mazeGen.ConnectExits(gridCenter, exitOffset);
            mazeGen.CreateWalls(gridWidth, gridHeight);
        }
        else if (biome is WaterBiome)
        {
            MazeGenerator mazeGen = new(gridMap);
            mazeGen.CreateClearings(gridWidth, gridHeight, 2, 18, false);
            mazeGen.ConnectExits(gridCenter, exitOffset);
            mazeGen.CreateWalls(gridWidth, gridHeight);
        }

        SpawnLevel(gridMap);
        //a* update
        AstarPath.active.Scan();
    }

    public GridSpaceType[,] PrepareGrid(int gridW, int gridH)
    {
        GridSpaceType[,] grid = new GridSpaceType[gridW, gridH];
        int lastCol = gridW - 1;
        int lastRow = gridH - 1;
        int centerCol = (int)gridCenter.x;
        int centerRow = (int)gridCenter.y;

        for (int row = 0; row < gridH; row++)
        {
            for (int col = 0; col < gridW; col++)
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
        for (int offset = -exitOffset; offset < exitOffset; offset++)
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
        Vector3Int pos;
        TileBase groundTile = biome.groundTile;
        TileBase wallTile = biome.wallTile;
        bool water = biome is WaterBiome;

        for (int col = 0; col < gridWidth; col++)
        {
            for (int row = 0; row < roomHeight / 2; row++)
            {
                switch (grid[col, row])
                {
                    case GridSpaceType.empty:
                        if (water)
                        {
                            // water biome wall tile is the same as the empty areas
                            // but needs to spawn on water layer
                            SpawnTile(col, row, wallTile, tileMapsArray[0]);
                        }
                        else SpawnTile(col, row, groundTile, tileMapsArray[1]);
                        break;
                    case GridSpaceType.floor:
                        //spawn = GameObject.FindGameObjectWithTag("Squrtal");

                        pos = SpawnTile(col, row, groundTile, tileMapsArray[1]);
                        //Instantiate(spawn, pos, Quaternion.identity);
                        
                        if( Random.Range(1, 10) ==1 && mobCap > mobcount){
                            if (inty == 1){
                                if (Random.Range(1,3)==1){
                                Instantiate(spawn, pos, Quaternion.identity);
                                } else {
                                    Instantiate(charmander, pos, Quaternion.identity);
                                }
                            }
                            
                        
                        // Debug.Log("i spawned at " + pos);
                        // Debug.Log(spawn.name);
                         mobcount=mobcount+1;

                        }
                        

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

    Vector3Int SpawnTile(int col, int row, TileBase tile, Tilemap map, bool twoTileWall = false)
    {
        // Offset the spawn coordinates by half the room size so it is centered properly
        // and subtract the roomOrigin to spawn in correct place
        Vector3Int offset = new(gridWidth, gridHeight, 0);
        offset.x -= (int)roomOrigin.x;
        offset.y -= (int)roomOrigin.y;
        Vector3Int ret = new Vector3Int(col * 2+1, row * 2+1, 0) - offset;

        // spawns the cell and its right, top, and top-right neighbors
        // this ensures all paths are at least two cells wide to make movement easier
        //  return Vector3Int(col * 2 + 1, row * 2+1, 0) - offset;
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
        return ret;
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
