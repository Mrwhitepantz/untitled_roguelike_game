using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static RoomBuilder Instantiate(this Object roomBuilder, RoomBuilder roomContainer, Vector3 position, Quaternion rotation, float noiseSeed)
    {
        RoomBuilder room = Object.Instantiate<RoomBuilder>(roomContainer, position, rotation) as RoomBuilder;
        room.noiseSeed = noiseSeed;
        return room;
    } 
}
