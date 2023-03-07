using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaze
{
    RoomBuilder.GridSpaceType[,] gridArray;

    public ForestMaze(RoomBuilder.GridSpaceType[,] grid)
    {
        gridArray = grid;
    }

    public class MazeClearing
    {
        public MazeClearing firstChild, secondChild;
        private const int minLeafSize = 3;
        public int X, Y, width, height;

        public MazeClearing(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            width = w;
            height = h;
        }

        public bool IsLeaf()
        {
            return firstChild == null && secondChild == null;
        }

        public bool Split()
        {
            if (!IsLeaf()) return false;

            // avoid making too wide or too tall by preferring to split in the larger direction
            // if close to same then pick random
            bool splitHorizontal;
            if(width / height >= 1.25)
            {
                splitHorizontal = false;
            }
            else if(height / width >= 1.25)
            {
                splitHorizontal = true;
            }
            else
            {
                splitHorizontal = Random.value > .5;
            }

            int maxSplit = (splitHorizontal ? height : width) - minLeafSize;
            if (maxSplit < minLeafSize) return false;

            int split = Random.Range(minLeafSize, maxSplit);

            if (splitHorizontal)
            {
                firstChild = new MazeClearing(X, Y, width, split);
                secondChild = new MazeClearing(X, Y + split, width, height - split);
            }
            else
            {
                firstChild = new MazeClearing(X, Y, split, height);
                secondChild = new MazeClearing(X + split, Y, width - split, height);
            }
            return true;
        }
        public void CreateFloors(RoomBuilder.GridSpaceType[,] gridArray)
        {
            if (!IsLeaf())
            {
                if(firstChild != null)
                {
                    firstChild.CreateFloors(gridArray);
                }
                if(secondChild != null)
                {
                    secondChild.CreateFloors(gridArray);
                }
            }
            else
            {
                Debug.Log("X:" + X + "Y:" + Y + "w:" + width + "h" + height);
                
                int roomX = Random.Range(X, X + width - minLeafSize - 1);
                int roomY = Random.Range(Y, Y + height - minLeafSize - 1);
                int roomWidth = Random.Range(minLeafSize, Mathf.Min(width, (gridArray.GetLength(0) - roomX)));
                int roomHeight = Random.Range(minLeafSize, Mathf.Min(height, (gridArray.GetLength(1) - roomY)));

                Debug.Log("rx:" + roomX + "ry:"+roomY+"rw:"+roomWidth+"rh"+roomHeight);

                for(int i = roomX; i < roomX + roomWidth - 1; i++)
                {
                    for(int j = roomY; j < roomY + roomHeight - 1; j++)
                    {
                        if(gridArray[i,j] == RoomBuilder.GridSpaceType.empty)
                        {
                            gridArray[i, j] = RoomBuilder.GridSpaceType.floor;
                        }
                        
                    }
                }
            }
        }
    }

    public void CreateWalls(int roomWidth, int roomHeight)
    {
        for (int i = 0; i < roomHeight; i++)
        {
            for(int j = 0; j < roomWidth; j++)
            {
                if(gridArray[i, j] == RoomBuilder.GridSpaceType.empty)
                {
                    gridArray[i, j] = RoomBuilder.GridSpaceType.wall;
                }
            }
        }
    }

    public void CreateClearings(int width, int height)
    {
        Debug.Log("Clearing");
        const int maxLeafSize = 12;
        List<MazeClearing> clearingList = new();
        MazeClearing root = new(0, 0, width, height);
        clearingList.Add(root);
        bool splitClearing = true;
        while (splitClearing)
        {
            splitClearing = false;
            for (int clearing = 0; clearing < clearingList.Count; clearing++)
            {
                // check that clearing is a leaf and if it's bigger than maxLeafSize
                if (clearingList[clearing].IsLeaf() && (clearingList[clearing].width > maxLeafSize || clearingList[clearing].height > maxLeafSize || Random.value < .25f))
                {
                    // check that it was able to be split
                    if (clearingList[clearing].Split())
                    {
                        clearingList.Add(clearingList[clearing].firstChild);
                        clearingList.Add(clearingList[clearing].secondChild);
                    }
                }
            }
        }
        root.CreateFloors(gridArray);
    }
}
