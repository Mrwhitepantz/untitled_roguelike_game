using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biome 
{
    public TileBase groundTile;
    public TileBase wallTile;
    public TileBase wallTile2;
    public List<Tile> objectTiles = new();
    public int wallTileCount = 1;

    public static Biome NewBiome(int humidityCoord, int temperatureCoord)
    {
        Texture2D biomeMap = Resources.Load<Texture2D>("Sprites/Environment/biome_map");
        
        Color mapPixel = biomeMap.GetPixel(humidityCoord, temperatureCoord);
        // the Color type holds rgba values as a float based on 256 color levels, 0-255.
        // multiplying by 255 and taking the floor returns the value as a color 
        // level the way it is normally seen in a color editor.
        switch (Mathf.FloorToInt(mapPixel.r * 255))
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
                return new ForestBiome();
        }
    }
    public Tile CreateTile(Texture2D texture)
    {
        Tile tile = Tile.CreateInstance<Tile>();
        tile.sprite = (Sprite.Create(texture,
                            new Rect(0, 0, 32, 32),
                            new Vector2(.5f, .5f),
                            32f)
                        );
        return tile;
    }
}
