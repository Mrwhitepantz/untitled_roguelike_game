using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowyBiome : Biome
{
    public SnowyBiome()
    {
        groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/snow"));
        wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/snow_boulder"));
        //objectTiles.Add(CreateTile(Resources.Load<Texture2D>("Sprites/Environment/bush")));
    }
}
