using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RoomBuilder : MonoBehaviour
{
    //public Biome biome;
    private readonly float moisture;
    private readonly float temperature;
    private static readonly int roomWidth = 40;
    private static readonly int roomHeight = 40;
    private Tilemap[] tileMaps;
    private static readonly int gridWidth = roomWidth / 2;
    private static readonly int gridHeight = roomHeight / 2;
    private Vector2 gridCenter = new(gridWidth / 2, gridHeight / 2);

    enum GridSpace { empty, floor, wall };
    

    // Start is called before the first frame update
    void Start()
    {
        // build a new room here
        BuildRoom(this.transform.position);
    }

    public void BuildRoom(Vector3 roomOrigin)
    {

        tileMaps = GetComponentsInChildren<Tilemap>();

        // creates a grid half the width/height of the final room so that forms are all 2x bigger for more space
        GridSpace[,] grid = new GridSpace[20, 20];

        // initialize every cell as empty
        for(int x=0; x < gridWidth; x++)
        {
            for(int y=0; y < gridHeight; y++)
            {
                if( x == 0 || y == 0 || x == gridWidth - 1 ||  y == gridHeight - 1)
                {
                    grid[x, y] = GridSpace.wall;
                }
                else
                {
                    grid[x, y] = GridSpace.empty;
                }
                
            }
        }

        // set the exits to floor
        for (int offset = -2; offset < 2; offset++)
        {
            // top
            grid[(int)gridCenter.x + offset, 0] = GridSpace.floor;
            // bottom
            grid[(int)gridCenter.x + offset, gridHeight - 1] = GridSpace.floor;
            // left
            grid[0, (int)gridCenter.y + offset] = GridSpace.floor;
            // right
            grid[gridWidth - 1, (int)gridCenter.y + offset] = GridSpace.floor;
        }

        SpawnLevel(grid);
        
    }

    void SpawnLevel(GridSpace[,] grid)
    {
        Tile groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/sand"));
        Tile wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/desert_boulder"));
        
        for (int x = 0; x < roomWidth / 2; x++)
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

    Tile CreateTile(Texture2D texture)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite.Create(texture, 
                            new Rect(0, 0, 32, 32), 
                            new Vector2(.5f, .5f), 
                            32f)
                        );
        return tile;
    }

    void SpawnTile(int x, int y, Tile tile, Tilemap map)
    {
        // Offset the spawn coordinates by half the room size so it is centered properly
        Vector3Int offset = new(roomWidth / 2, roomHeight / 2, 0);

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
        return 1f;
    }

    float GetTemperature(Vector3 roomOrigin)
    {
        return 1f;
    }
}
