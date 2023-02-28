using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaze : MazeGenerator
{
    private int gridWidth;
    private int gridHeight;
    private RoomBuilder.GridSpaceType[,] grid;
    private struct Leaf
    {
        int minimumSize;

    }
    public void NewMaze(int width, int height, RoomBuilder.GridSpaceType[,] gridArray)
    {
        gridWidth = width;
        gridHeight = height;
        grid = gridArray;
    }
    private void CreateFloors(RoomBuilder.GridSpaceType[,] grid)
    {
        CreateRooms(grid);
    }

    private void CreateWalls(int roomWidth, int roomHeight, RoomBuilder.GridSpaceType[,] gridArray)
    {

    }

    private void CreateRooms(RoomBuilder.GridSpaceType[,] grid)
    {
        
    }
}
