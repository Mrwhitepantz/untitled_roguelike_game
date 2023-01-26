using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    enum gridSpace {empty, floor, wall};
    private gridSpace[,] grid;
    private int roomHeight, roomWidth;
    private Vector2 roomSizeWorld = new Vector2(30, 30);
    private float worldUnitsToGridCell = 1;
    struct walker
    {
        public Vector2 dir;
        public Vector2 pos;
    }

    List<walker> walkers;
    private float chanceWalkerChangeDir = .5f;
    private float chanceWalkerSpawn = .05f;
    private float chanceWalkerDestroy = .05f;
    int maxWalkers = 10;
    float precentToFill = .2f;
    public GameObject wallObj, floorObj;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
        CreateFloors(); ;
        CreateWalls();
        RemoveSignleWalls();
        SpawnLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Setup()
    {
        // find grid size
        roomHeight = Mathf.RoundToInt(roomSizeWorld.x / worldUnitsToGridCell);
        roomWidth = Mathf.RoundToInt(roomSizeWorld.y / worldUnitsToGridCell);
        // create grid
        grid = new gridSpace[roomWidth, roomHeight];
        // set grid default state
        for(int x = 0; x < roomWidth - 1; x++)
        {
            for(int y=0; y < roomHeight - 1; y++)
            {
                // make every cell empty
                grid[x, y] = gridSpace.empty;
            }
        }
        // set first walker
        walkers = new List<walker>();
        // create walker
        walker newWalker = new walker();
        newWalker.dir = RandomDirection();
        // find center of grid
        Vector2 spawnPos = new Vector2(Mathf.RoundToInt(roomWidth / 2), Mathf.RoundToInt(roomHeight / 2));
        newWalker.pos = spawnPos;
        // add walker to list
        walkers.Add(newWalker);
    }

    Vector2 RandomDirection()
    {
        // pick random int beween 0 and 3
        int choice = Mathf.RoundToInt(Mathf.Floor(Random.Range(0,3)));
        // choose a direction
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
        }
    }

    void CreateFloors()
    {
        int iterations = 0; // don't let loop run forever
        do
        {
            // create floor at position of every walker
            foreach(walker myWalker in walkers)
            {
                grid[(int)myWalker.pos.x, (int)myWalker.pos.y] = gridSpace.floor;
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
             for(int i = 0; i < walkers.Count; i++)
             {
                 if(Random.value < chanceWalkerChangeDir)
                 {
                     walker thisWalker -walkers[i];
                     thisWalker.dir = RandomDirection();
                     walkers[i] = thisWalker;
                 }
             }

             // chance to spawn a new walker
            numChecks = walkers.Count;
            for(int i = 0; i < numChecks; i++)
            {
                // only if # of walkers < max, at low chance
                if(Random.value < chanceWalkerSpawn && walkers.Count < maxWalkers)
                {
                    // create a walker
                    walker newWalker = new walker();
                    newWalker.dir = RandomDirection();
                    newWalker.pos = walkers[i].pos;
                    walkers.Add(newWalker);
                }
            }

            // walk the walkers
            for(int i = 0; i < walkers.Count; i++)
            {
                walker thisWalker = walkers[i];
                thisWalker.pos += thisWalker.dir;
                walkers[i] = thisWalker;
            }

            // avoid border of the grid
            for(int i = 0; i < walkers.Count; i++)
            {
                walker thisWalker = walkers[i];
                // clamp x, y to leave a 1 space border and leave room for walls
                thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, roomWidth - 2);
                thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, roomHeight - 2);
                walkers[i] = thisWalker;
            }
        }
    }
}
