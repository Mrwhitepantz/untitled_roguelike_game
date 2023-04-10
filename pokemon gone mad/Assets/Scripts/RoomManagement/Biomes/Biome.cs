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
        return Mathf.FloorToInt(mapPixel.r * 255) switch
        {
            217 => new DesertBiome(),
            132 => new RockyBiome(),
            106 => new ForestBiome(),
            75 => new TropicalBiome(),
            203 => new SnowyBiome(),
            99 => new WaterBiome(),
            _ => new ForestBiome()
        };
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
