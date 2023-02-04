using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionManager : MonoBehaviour
{

    public Vector3 roomOffset;
    public Vector3 playerOffset;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private FollowPlayer camControl;
    private TopDownController playerController;
    private GameObject player;
    private GameObject roomSwitcher;

    // Panel/transition code from Mister Taft Creates https://www.youtube.com/watch?v=JcEJtEWjiZU

    // Start is called before the first frame update
    void Start()
    {
        camControl = Camera.main.GetComponent<FollowPlayer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = (TopDownController) player.GetComponent("TopDownController");
        roomSwitcher = GameObject.FindGameObjectWithTag("RoomSwitcher");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with this object
        if (collision.CompareTag("Player"))
        {
            // Fade out between rooms
            if(fadeOutPanel != null)
            {
                GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
                Destroy(panel, .35f);
            }

            playerController.pauseState = true;
            playerController.body.velocity = Vector3.zero;

            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        yield return new WaitForSeconds(fadeWait);
        RoomBuilder newRoom = new();
        newRoom.BuildRoom(roomSwitcher.transform.position + roomOffset);

        // change camera min/max positions to next room
        camControl.cameraMinPos += roomOffset;
        camControl.cameraMaxPos += roomOffset;
        // move player to next room
        player.transform.position += playerOffset;
        // move room switchers to next room
        roomSwitcher.transform.position += roomOffset;
        
        playerController.pauseState = false;
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, .35f);
        }
        
    }
}
