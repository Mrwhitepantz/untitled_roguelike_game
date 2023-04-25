using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTest : MonoBehaviour
{
    public int roomSize = 40;
    public int radius = 0;
    public RoomBuilder roomContainer;
    public bool active = false;
    public Grid worldGrid;
    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            float[] noiseList = new float[4];
            for (int i = 0; i < 4; i++)
            {
                noiseList[i] = Random.value;
            }
            Debug.Log(noiseList[0] + "\n" + noiseList[1] + "\n" + noiseList[2] + "\n" + noiseList[3]);
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if(x==0 && y == 0)
                    {
                        continue;
                    }

                    Vector3 pos = new(x * roomSize, y * roomSize, 0);
                    RoomBuilder room = Instantiate(roomContainer, pos, Quaternion.identity);

                    float[] manNoise = new float[] { .35f, .59f, .72f, .42f };
                    room.BuildRoom(noiseList, worldGrid);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
