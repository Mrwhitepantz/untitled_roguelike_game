using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Binary Space Partitioning process shown here: https://gamedevelopment.tutsplus.com/tutorials/how-to-use-bsp-trees-to-generate-game-maps--gamedev-12268

public class ForestMaze
{
    readonly RoomBuilder.GridSpaceType[,] gridArray;

    public ForestMaze(RoomBuilder.GridSpaceType[,] grid)
    {
        gridArray = grid;
    }

    public class MazeClearing
    {
        public MazeClearing firstChild, secondChild;
        private const int minLeafSize = 3;
        public int X, Y, width, height;
        public Vector4 room; // (x, y, z, w) = (x1, y1, x2, y2)

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

            // Find the greatest x or y value that we can safely split the clearing and
            // both its children are large enough to hold a clearing
            int maxSplit = (splitHorizontal ? height : width) - minLeafSize;
            if (maxSplit < minLeafSize) return false;

            // split the clearing somewhere between the min and max
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
                // this isn't a leaf, so go into children to create floors
                if(firstChild != null)
                {
                    firstChild.CreateFloors(gridArray);
                }
                if(secondChild != null)
                {
                    secondChild.CreateFloors(gridArray);
                }

                if(firstChild != null && secondChild != null)  // if both children exist here
                {
                    CreateHall(firstChild.GetRoom(), secondChild.GetRoom(), gridArray);
                }
            }
            else
            {   
                // fill a room in the clearing with (x,y) such that they are between
                // the clearing's (x,y) and the furthest point in the clearing that
                // the minimum room size can still be created inside it
                int roomX = Random.Range(X, X + width - minLeafSize - 1);
                int roomY = Random.Range(Y, Y + height - minLeafSize - 1);
                // width and height between the minimum size and the smaller of:
                // the clearing's width or height and the size of the entire cell
                // minus the room's position
                int roomWidth = Random.Range(minLeafSize, Mathf.Min(width, (gridArray.GetLength(0) - roomX)));
                int roomHeight = Random.Range(minLeafSize, Mathf.Min(height, (gridArray.GetLength(1) - roomY)));

                room = new Vector4(roomX, roomY, roomX + roomWidth - 1, roomY + roomHeight - 1);

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

        public Vector4 GetRoom()
        {
            if (room != Vector4.zero) return room;
            else
            {
                // recurse through the BSP tree until reaching a leaf which has a room in it
                Vector4 roomOne = new(), roomTwo = new();
                if (firstChild != null) roomOne = firstChild.GetRoom();
                if (secondChild != null) roomTwo = secondChild.GetRoom();

                // if there is only one child with a room, return that child, otherwise randomly pick
                if (roomOne == Vector4.zero) return roomTwo;
                else if (roomTwo == Vector4.zero) return roomOne;
                else if (Random.value > .5f) return roomTwo;
                else return roomOne;
            }
        }

        public void CreateHall(Vector4 roomOne, Vector4 roomTwo, RoomBuilder.GridSpaceType[,] gridArray)
        {
            Vector2Int roomOnePoint = new((int)Random.Range(roomOne.x, roomOne.z), (int) Random.Range(roomOne.y, roomOne.w)); 
            Vector2Int roomTwoPoint = new((int)Random.Range(roomTwo.x, roomTwo.z), (int)Random.Range(roomTwo.y, roomTwo.w));

            int length = roomTwoPoint.x - roomOnePoint.x;
            int height = roomTwoPoint.y - roomOnePoint.y;

            if (length < 0)  // roomTwo is start for horizontal
            {
                if (height < 0)  // roomTwo is start for vertical
                {
                    if (Random.value < .5f)  // pick horizontal first
                    {
                        // horizontal along start point y, then vertical along end point x
                        for (int x = roomTwoPoint.x; x <= roomOnePoint.x; x++) gridArray[x, roomTwoPoint.y] = RoomBuilder.GridSpaceType.floor;
                        for (int y = roomTwoPoint.y; y <= roomOnePoint.y; y++) gridArray[roomOnePoint.x, y] = RoomBuilder.GridSpaceType.floor;
                    }
                    else  // pick vertical first
                    {
                        // vertical along start point x, then horizontal along end point y
                        for (int y = roomTwoPoint.y; y <= roomOnePoint.y; y++) gridArray[roomTwoPoint.x, y] = RoomBuilder.GridSpaceType.floor;
                        for (int x = roomTwoPoint.x; x <= roomOnePoint.x; x++) gridArray[x, roomOnePoint.y] = RoomBuilder.GridSpaceType.floor;
                    }
                }
                else if (height > 0)  // roomOne is start for vertical
                {
                    if (Random.value < .5f)  // pick horizontal first
                    {
                        // horizontal along start point y, then vertical along end point x
                        for (int x = roomTwoPoint.x; x <= roomOnePoint.x; x++) gridArray[x, roomTwoPoint.y] = RoomBuilder.GridSpaceType.floor;
                        for (int y = roomOnePoint.y; y <= roomTwoPoint.y; y++) gridArray[roomOnePoint.x, y] = RoomBuilder.GridSpaceType.floor;
                    }
                    else  // pick vertical first
                    {
                        // vertical along start point x, then horizontal along end point y
                        for (int y = roomOnePoint.y; y <= roomTwoPoint.y; y++) gridArray[roomTwoPoint.x, y] = RoomBuilder.GridSpaceType.floor;
                        for (int x = roomTwoPoint.x; x <= roomOnePoint.x; x++) gridArray[x, roomOnePoint.y] = RoomBuilder.GridSpaceType.floor;
                    }
                }
                else  // same y coordinate
                {
                    for (int x = roomTwoPoint.x; x <= roomOnePoint.x; x++) gridArray[x, roomTwoPoint.y] = RoomBuilder.GridSpaceType.floor;
                }
            }
            else if (length > 0)  // roomOne is start for horizontal
            {
                if (height < 0)  // roomTwo is start for vertical
                {
                    if (Random.value < .5f)  // pick horizontal first
                    {
                        // horizontal along start point y, then vertical along end point x
                        for (int x = roomOnePoint.x; x <= roomTwoPoint.x; x++) gridArray[x, roomTwoPoint.y] = RoomBuilder.GridSpaceType.floor;
                        for (int y = roomTwoPoint.y; y <= roomOnePoint.y; y++) gridArray[roomOnePoint.x, y] = RoomBuilder.GridSpaceType.floor;
                    }
                    else  // pick vertical first
                    {
                        // vertical along start point x, then horizontal along end point y
                        for (int y = roomTwoPoint.y; y <= roomOnePoint.y; y++) gridArray[roomTwoPoint.x, y] = RoomBuilder.GridSpaceType.floor;
                        for (int x = roomOnePoint.x; x <= roomTwoPoint.x; x++) gridArray[x, roomOnePoint.y] = RoomBuilder.GridSpaceType.floor;
                    }
                }
                else if (height > 0)  // roomOne is start for vertical
                {
                    if (Random.value < .5f)  // pick horizontal first
                    {
                        // horizontal along start point y, then vertical along end point x
                        for (int x = roomOnePoint.x; x <= roomTwoPoint.x; x++) gridArray[x, roomTwoPoint.y] = RoomBuilder.GridSpaceType.floor;
                        for (int y = roomOnePoint.y; y <= roomTwoPoint.y; y++) gridArray[roomOnePoint.x, y] = RoomBuilder.GridSpaceType.floor;
                    }
                    else  // pick vertical first
                    {
                        // vertical along start point x, then horizontal along end point y
                        for (int y = roomOnePoint.y; y <= roomTwoPoint.y; y++) gridArray[roomTwoPoint.x, y] = RoomBuilder.GridSpaceType.floor;
                        for (int x = roomOnePoint.x; x <= roomTwoPoint.x; x++) gridArray[x, roomOnePoint.y] = RoomBuilder.GridSpaceType.floor;
                    }
                }
                else  // same y coordinate
                {
                    for (int x = roomOnePoint.x; x <= roomTwoPoint.x; x++) gridArray[x, roomTwoPoint.y] = RoomBuilder.GridSpaceType.floor;
                }
            }
            else  // same x coordinate
            {
                if(height < 0)
                {
                    for (int y = roomTwoPoint.y; y <= roomOnePoint.y; y++) gridArray[roomOnePoint.x, y] = RoomBuilder.GridSpaceType.floor;
                }
                else if(height > 0)
                {
                    for (int y = roomOnePoint.y; y <= roomTwoPoint.y; y++) gridArray[roomOnePoint.x, y] = RoomBuilder.GridSpaceType.floor;
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

    public void ConnectExits(Vector2Int center, int offset)
    {
        // (x, y, z) = (-side of exit, +side of exit, row/column exit is in
        Vector3Int bot = new(center.x - offset, center.x + offset, 0);
        Vector3Int top = new(center.x - offset, center.x + offset, gridArray.GetLength(1) - 1);
        Vector3Int left = new(center.y - offset, center.y + offset, 0);
        Vector3Int right = new(center.y - offset, center.y + offset, gridArray.GetLength(0) - 1);

        int randomBot = Random.Range(bot.x, bot.y);
        for(int j = bot.z + 1; j < gridArray.GetLength(1); j++)
        {
            if (gridArray[randomBot, j] == RoomBuilder.GridSpaceType.empty)
            {
                gridArray[randomBot, j] = RoomBuilder.GridSpaceType.floor;
            }
            else break;
        }

        int randomTop = Random.Range(top.x, top.y);
        for (int j = top.z - 1; j >= 0; j--)
        {
            if (gridArray[randomTop, j] == RoomBuilder.GridSpaceType.empty)
            {
                gridArray[randomTop, j] = RoomBuilder.GridSpaceType.floor;
            }
            else break;
        }

        int randomLeft = Random.Range(left.x, left.y);
        for (int i = left.z + 1; i < gridArray.GetLength(0); i++)
        {
            if (gridArray[i, randomLeft] == RoomBuilder.GridSpaceType.empty)
            {
                gridArray[i, randomLeft] = RoomBuilder.GridSpaceType.floor;
            }
            else break;
        }

        int randomRight = Random.Range(right.x, right.y);
        for (int i = right.z - 1; i >= 0; i--)
        {
            if (gridArray[i, randomRight] == RoomBuilder.GridSpaceType.empty)
            {
                gridArray[i, randomRight] = RoomBuilder.GridSpaceType.floor;
            }
            else break;
        }

    }

    public void CreateClearings(int width, int height)
    {
        const int maxLeafSize = 12;
        List<MazeClearing> clearingList = new();
        MazeClearing root = new(1, 1, width, height);
        clearingList.Add(root);

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
        root.CreateFloors(gridArray);
    }
}