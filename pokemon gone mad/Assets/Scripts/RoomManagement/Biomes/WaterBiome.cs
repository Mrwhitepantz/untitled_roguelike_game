using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterBiome : Biome
{
    public WaterBiome()
    {
        groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/boardwalk"));
        wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/waterS"));
        //wallTile = Resources.Load<TileBase>("Sprites/Environment/waterA");
        //objectTiles.Add(CreateTile(Resources.Load<Texture2D>("Sprites/Environment/bush")));
    }
}
