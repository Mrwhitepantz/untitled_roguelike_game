using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileRotationManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tileMap;

    private int rotationMult;

    // Start is called before the first frame update
    void Start()
    {
        RotateTiles(tileMap);
    }
    
    public void RotateTiles(Tilemap map)
    {
        // Get each tile's position and use that to randomly set the rotation
        // of the tile in 90d increments 
        map.CompressBounds();
        
        foreach (Vector3Int pos in map.cellBounds.allPositionsWithin)
        {
            Debug.Log(pos);
            rotationMult = Random.Range(0, 4);
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f * rotationMult), Vector3.one);
            string tileName = map.GetTile(pos).ToString();

            if (tileName.StartsWith("grass"))
            {
                map.SetTransformMatrix(pos, matrix);
            }

        }
    }
}
