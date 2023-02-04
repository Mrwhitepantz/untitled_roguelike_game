using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 camOffset = new(0, 0, -10);
    public Vector3 cameraMaxPos, cameraMinPos;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = player.transform.position + camOffset;
        newPos.x = Mathf.Clamp(newPos.x, cameraMinPos.x, cameraMaxPos.x);
        newPos.y = Mathf.Clamp(newPos.y, cameraMinPos.y, cameraMaxPos.y);

        transform.position = newPos;
    }
}
