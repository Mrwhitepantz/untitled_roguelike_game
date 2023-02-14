using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropicalBiome : Biome
{
    public TropicalBiome()
    {
        groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/sand"));
        wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/palm_tree_bottom"));
        wallTile2 = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/palm_tree_top"));
        //objectTiles.Add(CreateTile(Resources.Load<Texture2D>("Sprites/Environment/bush")));
        wallTileCount = 2;
    }
}
