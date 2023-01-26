using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 camOffset = new Vector3(0, 0, -10);
    public Camera cam;
    enum edges { LEFT, RIGHT, TOP, BOTTOM }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + camOffset;
    }
}
