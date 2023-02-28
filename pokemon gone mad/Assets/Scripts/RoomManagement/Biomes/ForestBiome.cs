using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBiome : Biome
{
    public ForestBiome()
    {
        groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/grass"));
        wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/tree_bottom"));
        wallTile2 = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/tree_top"));
        objectTiles.Add(CreateTile(Resources.Load<Texture2D>("Sprites/Environment/bush")));
        wallTileCount = 2;
    }
}
