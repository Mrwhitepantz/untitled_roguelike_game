using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertBiome : Biome
{
    public DesertBiome()
    {
        groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/sand"));
        wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/desert_boulder"));
        //objectTiles.Add(CreateTile(Resources.Load<Texture2D>("Sprites/Environment/bush")));
    }
}
