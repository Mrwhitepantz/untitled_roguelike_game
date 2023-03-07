using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockyBiome : Biome
{
    public RockyBiome()
    {
        groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/rock"));
        wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/boulder"));
        objectTiles.Add(CreateTile(Resources.Load<Texture2D>("Sprites/Environment/boulder")));
    }
}
