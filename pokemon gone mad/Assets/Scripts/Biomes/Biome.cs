using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biome 
{
    public Tile groundTile;
    public Tile wallTile;
    public Tile wallTile2;
    public List<Tile> objectTiles = new();
    public int wallTileCount = 1;

    public static Biome NewBiome(int moisture, int temperature)
    {
        Texture2D map = Resources.Load<Texture2D>("Sprites/Environment/biome_map");
        
        Color rgba = map.GetPixel(moisture, temperature);
        Debug.Log(rgba.r);
        switch (Mathf.FloorToInt(rgba.r * 255))
        {
            case (217):      //Desert
                Debug.Log("Desert");
                return new DesertBiome();
            case (132):     //Rocky
                Debug.Log("Rocky");
                return new RockyBiome();
            case (106):     //Forest
                return new ForestBiome();
            case (75):      //Tropical
                Debug.Log("Tropical");
                return new TropicalBiome();
            case (203):     //Snowy
                Debug.Log("Snowy");
                return new SnowyBiome();
            case (99):      //Water
                Debug.Log("Water");
                return new WaterBiome();
            default:
                return new DesertBiome();
        }
    }
    public Tile CreateTile(Texture2D texture)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite.Create(texture,
                            new Rect(0, 0, 32, 32),
                            new Vector2(.5f, .5f),
                            32f)
                        );
        return tile;
    }
}
