using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTest : MonoBehaviour
{
    public int roomSize = 40;
    public int radius = 0;
    public RoomBuilder roomContainer;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int y = -radius; y < radius; y++)
                {
                    Vector3 pos = new(x * roomSize, y * roomSize, 0);
                    Instantiate(roomContainer, pos, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
