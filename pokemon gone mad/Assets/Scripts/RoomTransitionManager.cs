using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeInPanel;
    [SerializeField]
    private GameObject fadeOutPanel;
    [SerializeField]
    private float fadeWait;
    [SerializeField]
    private RoomBuilder roomContainer;

    private FollowPlayer camControl;
    private TopDownController playerController;
    private GameObject player;
    private Vector3 roomOffset, playerOffset;
    private List<Vector3> roomList = new();

    // Panel/transition code from Mister Taft Creates https://www.youtube.com/watch?v=JcEJtEWjiZU

    // Start is called before the first frame update
    void Start()
    {
        // Assigns scene objects and adds first room to the roomList
        camControl = Camera.main.GetComponent<FollowPlayer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = (TopDownController) player.GetComponent("TopDownController");
        roomList.Add(new Vector3(0, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Starts the transition beteen rooms and begins the coroutine
    public void StartTransition(Vector3 roomMove, Vector3 playerMove)
    {
        roomOffset = roomMove;
        playerOffset = playerMove;

        // Fade out between rooms
        if(fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, .35f);
        }

        // put the player in a pause state so they can't move
        playerController.pauseState = true;
        playerController.body.velocity = Vector3.zero;

        StartCoroutine(TransitionCo());
    }

    public IEnumerator TransitionCo()
    {
        yield return new WaitForSeconds(fadeWait);

        // move room switchers to next room
        this.transform.position += roomOffset;
        // if the next room isn't in the roomList, add it and make a new RoomBuilder
        if(!roomList.Contains(this.transform.position))
        {
            roomList.Add(this.transform.position);
            Instantiate(roomContainer, this.transform.position, Quaternion.identity);
        }
        
        // change camera min/max positions to next room
        camControl.cameraMinPos += roomOffset;
        camControl.cameraMaxPos += roomOffset;
        // move player to next room
        player.transform.position += playerOffset;
        
        // take the player out of the pause state
        playerController.pauseState = false;

        // fade back in
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, .35f);
        }
        
    }
}
