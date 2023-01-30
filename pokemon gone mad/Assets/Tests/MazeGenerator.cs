using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallObjBot, wallObjTop, floorObj;
    public static Vector2 roomSizeWorld = new(20, 20);
    public float chanceWalkerChangeDir = .5f;
    public float chanceWalkerSpawn = .05f;
    public float chanceWalkerDestroy = .05f;
    public int maxWalkers = 10;
    public float percentToFill = .3f;
    public int maxIterations = 100000;
    public float chanceToMiss = 0.05f;

    private enum GridSpace {empty, floor, wall};
    private GridSpace[,] grid;
    private static readonly float worldUnitsToGridCell = 1;
    private static readonly int roomHeight = Mathf.RoundToInt(roomSizeWorld.x / worldUnitsToGridCell);
    private static readonly int roomWidth = Mathf.RoundToInt(roomSizeWorld.y / worldUnitsToGridCell);
    private static Vector2 roomCenter = new(Mathf.RoundToInt(roomWidth / 2), Mathf.RoundToInt(roomHeight / 2));


    struct Walker
    {
        public Vector2 dir;
        public Vector2 pos;
    }

    private List<Walker> walkers;

    // Algorithm from Six Dot https://www.youtube.com/watch?v=I74I_MhZIK8

    // Start is called before the first frame update
    void Start()
    {
        Setup();
        CreateFloors(); ;
        CreateWalls();
        // RemoveSingleWalls();
        ConnectExits();
        SpawnLevel();
    }

    void Setup()
    {
        // create grid
        grid = new GridSpace[roomWidth, roomHeight];
        // set grid default state
        for(int x = 0; x < roomWidth - 1; x++)
        {
            for(int y=0; y < roomHeight - 1; y++)
            {
                // make every cell empty
                grid[x, y] = GridSpace.empty;
            }
        }
        // set first walker
        walkers = new List<Walker>();
        AddWalker(roomCenter, RandomDirection(), walkers);
        AddWalker(roomCenter, RandomDirection(), walkers);
    }

    void AddWalker(Vector2 spawnPos, Vector2 dir, List<Walker> walkers)
    {
        Walker newWalker = new()
        {
            dir = dir,
            pos = spawnPos
        };
        walkers.Add(newWalker);
    }

    Vector2 RandomDirection()
    {
        // pick random int beween 0 and 3
        int choice = Random.Range(0,4);
        // choose a direction
        return choice switch
        {
            0 => Vector2.down,
            1 => Vector2.left,
            2 => Vector2.up,
            _ => Vector2.right,
        };
    }

    void CreateFloors()
    {
        int iterations = 0; // don't let loop run forever
        do
        {
            // create floor at position of every walker
            foreach (Walker myWalker in walkers)
            {
                grid[(int)myWalker.pos.x, (int)myWalker.pos.y] = GridSpace.floor;
            }
            // chance to destroy walker
            int numChecks = walkers.Count; // in case count is modified while in loop
            for (int i = 0; i < numChecks; i++)
            {
                // if it's not the only one, and low chance
                if (Random.value < chanceWalkerDestroy && walkers.Count > 1)
                {
                    walkers.RemoveAt(i);
                    break; // so only one is removed per loop
                }
            }

            // chance for walker to pick new direction
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerChangeDir)
                {
                    Walker thisWalker = walkers[i];
                    thisWalker.dir = RandomDirection();
                    walkers[i] = thisWalker;
                }
            }

            // chance to spawn a new walker
            numChecks = walkers.Count;
            for (int i = 0; i < numChecks; i++)
            {
                // only if # of walkers < max, at low chance
                if (Random.value < chanceWalkerSpawn && walkers.Count < maxWalkers)
                {
                    // create a walker
                    AddWalker(walkers[i].pos, walkers[i].dir, walkers);
                }
            }

            // walk the walkers
            Walk(walkers);

            // avoid border of the grid
            for (int i = 0; i < walkers.Count; i++)
            {
                Walker thisWalker = walkers[i];
                // clamp x, y to leave a 1 space border and leave room for walls
                thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, roomWidth - 2);
                thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, roomHeight - 2);
                walkers[i] = thisWalker;
            }

            // exit the loop
            if ((float)NumberOfFloors() / (float)grid.Length > percentToFill)
            {
                break;
            }
            iterations++;
        } while (iterations < maxIterations);
    }

    void Walk(List<Walker> walkers)
    {
        for (int i = 0; i < walkers.Count; i++)
        {
            Walker thisWalker = walkers[i];
            thisWalker.pos += thisWalker.dir;
            walkers[i] = thisWalker;
        }
    }

    int NumberOfFloors()
    {
        int count = 0;
        foreach(GridSpace space in grid)
        {
            if(space == GridSpace.floor)
            {
                count++;
            }
        }
        return count;
    }

    void CreateWalls()
    {
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                if (grid[x, y] == GridSpace.floor)
                {
                    if(grid[x,y+1] == GridSpace.empty)
                    {
                        grid[x, y+1] = GridSpace.wall;
                    }
                    if (grid[x, y-1] == GridSpace.empty)
                    {
                        grid[x, y-1] = GridSpace.wall;
                    }
                    if (grid[x+1, y] == GridSpace.empty)
                    {
                        grid[x+1, y] = GridSpace.wall;
                    }
                    if (grid[x-1, y] == GridSpace.empty)
                    {
                        grid[x-1, y] = GridSpace.wall;
                    }
                }
            }
        }
    }

    // My Code ==============================
    void ConnectExits()
    {
        List<Walker> exitWalkers = new();

        // Add exit walkers
        for (int offset = -2; offset < 2; offset++)
        {
            // top
            AddWalker(new Vector2(roomCenter.x + offset, 0), Vector2.up, exitWalkers);
            // bottom
            AddWalker(new Vector2(roomCenter.x + offset, roomHeight - 1), Vector2.down, exitWalkers);
            // left
            AddWalker(new Vector2(0, roomCenter.y + offset), Vector2.right, exitWalkers);
            // right
            AddWalker(new Vector2(roomWidth - 1, roomCenter.y + offset), Vector2.left, exitWalkers);
        }

        do
        {
            for (int i = 0; i < exitWalkers.Count; i++)
            {
                Debug.Log(exitWalkers[i].pos);
                if (grid[(int)exitWalkers[i].pos.x, (int)exitWalkers[i].pos.y] == GridSpace.floor)
                {
                    Debug.Log(exitWalkers[i].pos);
                    exitWalkers.RemoveAt(i);
                }
                else
                {
                    grid[(int)exitWalkers[i].pos.x, (int)exitWalkers[i].pos.y] = GridSpace.floor;
                }
            }
            Walk(exitWalkers);
        } while (exitWalkers.Count > 0);
    }
    // End My Code ==========================
    void SpawnLevel()
    {
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                Spawn(x, y, false);
                switch (grid[x, y])
                {
                    case GridSpace.empty:
                        Spawn(x, y, true);
                        break;
                    case GridSpace.floor:
                        break;
                    case GridSpace.wall:
                        Spawn(x, y, true);
                        break;
                }
            }
        }
    }

    void Spawn(float x, float y, bool wall)
    {
        // find the position to spawn
        Vector2 offset = roomSizeWorld;
        Quaternion objRot;

        for (int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                Vector2 spawnPos = new Vector2(x * 2 + i, y * 2 + j) * worldUnitsToGridCell - offset;
                // spawn the object

                // My Code ==========================
                if (wall)
                {
                    objRot = Quaternion.identity;
                    spawnPos.x += Random.Range(-1, 1) / 4.0f;
                    spawnPos.y += Random.Range(-1, 1) / 4.0f;
                    if (Random.value > chanceToMiss)
                    {
                        Instantiate(wallObjBot, spawnPos, objRot);
                        spawnPos.y += 1;
                        Instantiate(wallObjTop, spawnPos, objRot);
                    }
                }
                else
                {
                    float rotationMult = Mathf.Floor(Random.value * 4);
                    objRot = Quaternion.Euler(0f, 0f, 90f * rotationMult);
                    Instantiate(floorObj, spawnPos, objRot);
                }
                // End My Code ======================
            }

        }
    }
}
