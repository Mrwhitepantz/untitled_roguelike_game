using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biome 
{
    public Tile groundTile;
    public Tile wallTile;
    //public Tile[] tileObjects;
    public int biomeID;

    public Biome(float moisture, float temperature)
    {
        int coordX = Mathf.FloorToInt(moisture * 10);
        int coordY = Mathf.FloorToInt(temperature * 10);
        Texture2D map = Resources.Load<Texture2D>("Sprites/Environment/biome_map");
        int biomeID = GetBiomeIDFromCoordinates(coordX,coordY,map);

        Debug.Log("Setting tiles: ");
        switch (biomeID)
        {
            case (1):
                groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/sand"));
                wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/desert_boulder"));
                break;
            case (2):
                groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/rock"));
                wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/boulder"));
                break;
            case (3):
                groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/grass"));
                wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/tree_bottom"));
                break;
            case (4):
                groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/sand"));
                wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/palm_tree_bottom"));
                break;
            case (5):
                groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/snow"));
                wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/snow_boulder"));
                break;
            case (6):
                groundTile = CreateTile(Resources.Load<Texture2D>("Sprites/GroundTiles/boardwalk"));
                wallTile = CreateTile(Resources.Load<Texture2D>("Sprites/Environment/water"));
                break;
            default:
                break;
        }
    }

    private int GetBiomeIDFromCoordinates(int coordX, int coordY, Texture2D map)
    {
        Color rgba = map.GetPixel(coordX, coordY);
        switch (Mathf.FloorToInt(rgba.r * 255))
        {
            case (217):      //Desert
                Debug.Log("Desert");
                return 1;
            case (132):     //Rocky
                Debug.Log("Rocky");
                return 2;
            case (106):     //Forest
                Debug.Log("Forest");
                return 3;
            case (75):      //Tropical
                Debug.Log("Tropical");
                return 4;
            case (203):     //Snowy
                Debug.Log("Snowy");
                return 5;
            case (99):      //Water
                Debug.Log("Water");
                return 6;
            default:
                return 3;
        }
    }

    Tile CreateTile(Texture2D texture)
    {
        Debug.Log("Making Tile");
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite.Create(texture,
                            new Rect(0, 0, 32, 32),
                            new Vector2(.5f, .5f),
                            32f)
                        );
        return tile;
    }
}
