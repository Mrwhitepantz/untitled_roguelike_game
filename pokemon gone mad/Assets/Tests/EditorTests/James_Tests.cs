using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class James_Tests
{
    // Blackbox Biome tests
    [Test]
    public void TestBiomeTypeIsForestAt50_50() // Acceptance Test
    {
        Biome testBiome = Biome.NewBiome(50, 50);
        Assert.True(testBiome is ForestBiome);
    }
    [Test]
    public void TestBiomeTypeIsDesertAt0_100() // Acceptance Test
    {
        Biome testBiome = Biome.NewBiome(0, 100);
        Assert.True(testBiome is DesertBiome); ;
    }
    [Test]
    public void TestBiomeTypeIsRockyAt0_0() // Acceptance Test
    {
        Biome testBiome = Biome.NewBiome(0, 0);
        Assert.True(testBiome is RockyBiome);
    }
    [Test]
    public void TestBiomeTypeIsSnowyAt75_0() // Acceptance Test
    {
        Biome testBiome = Biome.NewBiome(75, 0);
        Assert.True(testBiome is SnowyBiome);
    }
    [Test]
    public void TestBiomeTypeIsTropicalAt75_100() // Acceptance Test
    {
        Biome testBiome = Biome.NewBiome(75, 100);
        Assert.True(testBiome is TropicalBiome);
    }
    [Test]
    public void TestBiomeTypeIsWaterAt100_50() // Acceptance Test
    {
        Biome testBiome = Biome.NewBiome(100, 50);
        Assert.True(testBiome is WaterBiome);
    }

    // Whitebox MazeClearing test
    [Test]
    public void TestMazeClearingIsLeafMethod() // along with other TestMazeClearing tests, achieves function coverage
     {
        ForestMaze.MazeClearing testClearing = new(0, 0, 10, 10); // makes split horizontal random
        Assert.True(testClearing.IsLeaf());
    }
    [Test]
    public void TestMazeClearingSplitMethodDidSplit() // along with other TestMazeClearing tests, achieves function coverage
    {
        ForestMaze.MazeClearing testClearing = new(0, 0, 10, 10); // makes split horizontal random
        Assert.True(testClearing.Split());
    }
    [Test]
    public void TestMazeClearingSplitMethodDidNotSplit() // along with other TestMazeClearing tests, achieves function coverage
     {
        ForestMaze.MazeClearing testClearingSmall = new(0, 0, 5, 5); // makes maxSplit < minLeafSize
        Assert.False(testClearingSmall.Split());
    }
    [Test]
    public void TestMazeClearingCreateFloorsMethod() // along with other TestMazeClearing tests, achieves function coverage
       {
        ForestMaze.MazeClearing testClearing = new(0, 0, 5, 5);
        RoomBuilder.GridSpaceType[,] testGrid = new RoomBuilder.GridSpaceType[5, 5];
        testClearing.CreateFloors(testGrid);
        bool floorMade = false;
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if(testGrid[i,j] == RoomBuilder.GridSpaceType.floor)
                {
                    floorMade = true;
                }
            }
        }
        Assert.True(floorMade);
    }
    [Test]
    public void TestMazeClearingGetRoomMethod() // along with other TestMazeClearing tests, achieves function coverage
    {
        ForestMaze.MazeClearing testClearing = new(0, 0, 10, 10);
        testClearing.firstChild = new(0, 0, 6, 6);
        testClearing.firstChild.room = new Vector4(1, 1, 4, 4);
        Assert.That(testClearing.GetRoom(), Is.EqualTo(new Vector4(1, 1, 4, 4)));
    }
    [Test]
    public void TestMazeClearingCreateHallMethod() // along with other TestMazeClearing tests, achieves function coverage
    {
        ForestMaze.MazeClearing testClearing = new(0, 0, 5, 5);
        RoomBuilder.GridSpaceType[,] testGrid = new RoomBuilder.GridSpaceType[5, 5];
        testClearing.CreateHall(new Vector4(0, 0, 2, 2), new Vector4(2, 2, 2, 2), testGrid);
        bool hallMade = false;
        // check over all the spaces in the grid for floors
        // a hall should be an unbroken set of orthogonally adjacent grid cells set to floor
        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                if (testGrid[i, j] == RoomBuilder.GridSpaceType.floor)
                {
                    bool adjacentFloor = false;
                    // check over the orthogonally adjacent cells for other floors
                    if(testGrid[i - 1, j] == RoomBuilder.GridSpaceType.floor ||
                       testGrid[i + 1, j] == RoomBuilder.GridSpaceType.floor ||
                       testGrid[i, j - 1] == RoomBuilder.GridSpaceType.floor ||
                       testGrid[i, j + 1] == RoomBuilder.GridSpaceType.floor)
                    {
                        adjacentFloor = true;
                    }
                    if (!adjacentFloor) // if there's not an adjacent floor, the hall is broken
                    {
                        hallMade = false;
                        break;
                    }
                    hallMade = true;
                }
            }
        }
        Assert.True(hallMade);
    }

    // Integration Test for RoomManager and RoomBuilder
    public readonly Dictionary<Vector3, Biome> roomDictionary = new(); // This dictionary normally uses RoomBuilders as values
    private readonly float[] noiseSeedArray = new float[4];
    [Test]
    public void TestRoomManagerRoomBuilderIntegraton()
    /*
     public void CreateAndAddRoom()
        {
            RoomBuilder newRoom = Instantiate(roomContainer, this.transform.position, Quaternion.identity);
            roomDictionary.Add(this.transform.position, newRoom);
            newRoom.BuildRoom(noiseSeedArray, worldGrid);
        } 
    public void BuildRoom(float[] noiseSeedArray, Grid grid)
        {
            // build a new room here
            GridSpaceType[,] gridMap = PrepareGrid(gridWidth, gridHeight);
            roomOrigin = this.transform.position;

            humidity = GetHumidity(new float[] { noiseSeedArray[0], noiseSeedArray[1] });
            temperature = GetTemperature(new float[] { noiseSeedArray[2], noiseSeedArray[3] });
            biome = Biome.NewBiome(humidity, temperature);
        }
     */
    {
        // initialize noiseSeeds adjusting perlin noise
        for (int i = 0; i < 4; i++) 
        {
            noiseSeedArray[i] = Random.value;
        }
        Assert.False(roomDictionary.ContainsKey(new Vector3(0, 0, 1)));
        RoomManagerCreateAndAddLogic(new Vector3(0, 0, 1));
        Assert.True(roomDictionary[new Vector3(0, 0, 1)] is Biome);
    }

    void RoomManagerCreateAndAddLogic(Vector3 pos)
    // Simulates call to RoomManager.CreateAndAddRoom() to create a room
    // and add it to the room dictionary
    {
        if (!roomDictionary.ContainsKey(pos))
        {
            roomDictionary.Add(pos, BuildRoomLogic(noiseSeedArray, pos));
        }
    }

    Biome BuildRoomLogic(float[] noiseSeedArray, Vector3 pos)
    // Simulates call to RoomBuilder.BuildRoom() to build a room with a Biome
    // based on position and humidity/temperature values with random noise seed
    {
        Vector3 roomOrigin = pos;
        int humidity = GetHumidity(new float[] { noiseSeedArray[0], noiseSeedArray[1] }, roomOrigin, 40, 40, 3f);
        int temperature = GetTemperature(new float[] { noiseSeedArray[2], noiseSeedArray[3] }, roomOrigin, 40, 40, 3f);
        return Biome.NewBiome(humidity, temperature);
    }

    int GetHumidity(float[] noiseSeeds, Vector3 roomOrigin,int roomWidth, int roomHeight, float perlinScale)
    {
        float noiseX = noiseSeeds[1] * roomOrigin.x / roomWidth;
        float noiseY = noiseSeeds[0] * roomOrigin.y / roomHeight;

        float perlinRaw = Mathf.PerlinNoise((noiseX + noiseSeeds[0]) / perlinScale, (noiseY + noiseSeeds[1]) / perlinScale);
        // PerlinNoise can return values slightly below 0 or above 1,
        // so needs to be clamped to ensure mapping works correctly
        float perlinClamp = Mathf.Clamp(perlinRaw, 0, .9999999f);
        return Mathf.FloorToInt(perlinClamp * 100);
    }

    int GetTemperature(float[] noiseSeeds, Vector3 roomOrigin, int roomWidth, int roomHeight, float perlinScale)
    {
        float noiseX = noiseSeeds[1] * roomOrigin.x / roomWidth;
        float noiseY = noiseSeeds[0] * roomOrigin.y / roomHeight;

        float perlinRaw = Mathf.PerlinNoise((noiseX + noiseSeeds[0]) / perlinScale, (noiseY + noiseSeeds[1]) / perlinScale);
        // PerlinNoise can return values slightly below 0 or above 1,
        // so needs to be clamped to ensure mapping works correctly
        float perlinClamp = Mathf.Clamp(perlinRaw, 0, .9999999f);
        return Mathf.FloorToInt(perlinClamp * 100);
    }
}
