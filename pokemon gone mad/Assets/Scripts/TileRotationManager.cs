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
        // Get each tile's position and use that to randomly set the rotation
        // of the tile in 90d increments 

        foreach (Vector3Int pos in tileMap.cellBounds.allPositionsWithin)
        {
            rotationMult = Random.Range(0, 4);
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f * rotationMult), Vector3.one);

            string tileName = tileMap.GetTile(pos).ToString();

            if (tileName.StartsWith("grass")){
                tileMap.SetTransformMatrix(pos, matrix);
            }
                
        }
    }

}
