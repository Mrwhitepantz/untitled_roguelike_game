using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionSwitch : MonoBehaviour
{

    public Vector3 roomOffset;
    public Vector3 playerOffset;
    public RoomManager roomSwitcher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with this object
        if (collision.CompareTag("Player"))
        {
            roomSwitcher.StartTransition(roomOffset, playerOffset);
        }
    }
}
