using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBiome : Biome
{
    public WaterBiome()
    {
        groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/boardwalk"));
        wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/Water_Anim"));
        //objectTiles.Add(CreateTile(Resources.Load<Texture2D>("Sprites/Environment/bush")));
    }
}
