using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Grid worldGrid;
    
    [SerializeField]
    private GameObject fadeInPanel;
    [SerializeField]
    private GameObject fadeOutPanel;
    [SerializeField]
    private float fadeWaitSeconds = .5f;
    [SerializeField]
    private RoomBuilder roomContainer;

    private FollowPlayer cameraController;
    private TopDownController playerController;
    private GameObject playerObject;
    private Vector3 currentRoomPosition;
    private Vector3 roomOffset, playerOffset;
    private readonly Dictionary<Vector3, RoomBuilder> roomDictionary = new();
    private readonly float[] noiseSeedArray = new float[4];

    // Panel/transition code from Mister Taft Creates https://www.youtube.com/watch?v=JcEJtEWjiZU

    // Start is called before the first frame update
    void Start()
    {
        // Assign scene objects
        cameraController = Camera.main.GetComponent<FollowPlayer>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = (TopDownController) playerObject.GetComponent("TopDownController");
        currentRoomPosition = this.transform.position;

        // add first room to the roomList
        RoomBuilder firstRoom = Instantiate(roomContainer, currentRoomPosition, Quaternion.identity);
        firstRoom.biome = new ForestBiome();
        roomDictionary.Add(new Vector3(0, 0, 1), firstRoom);

        // initialize noiseSeeds adjusting perlin noise
        for(int i =0; i < 4; i++)
        {
            noiseSeedArray[i] = Random.value;
        }
    }

    public Biome GetRoomBiome()
    {
        return roomDictionary[currentRoomPosition].biome;
    }

    // Starts the transition beteen rooms and begins the coroutine
    public void StartTransition(Vector3 roomPositionOffset, Vector3 playerPositionOffset)
    {
        roomOffset = roomPositionOffset;
        playerOffset = playerPositionOffset;

        // Fade out between rooms
        if(fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, .35f);
        }

        // put the player in a pause state so they can't move
        playerController.pauseState = true;
        playerController.body.velocity = Vector3.zero;

        StartCoroutine(TransitionCoroutine());
    }

    public IEnumerator TransitionCoroutine()
    {
        yield return new WaitForSeconds(fadeWaitSeconds);

        // move room switchers to next room
        currentRoomPosition += roomOffset;
        // if the next room isn't in the roomList, make a new RoomBuilder and add it
        if(!roomDictionary.ContainsKey(currentRoomPosition))
        {
            RoomBuilder newRoom = Instantiate(roomContainer, currentRoomPosition, Quaternion.identity);
            roomDictionary.Add(currentRoomPosition, newRoom);
            newRoom.BuildRoom(noiseSeedArray, worldGrid);
        }
        
        // change camera min/max positions to next room
        cameraController.cameraMinPos += roomOffset;
        cameraController.cameraMaxPos += roomOffset;
        // move player to next room
        playerObject.transform.position += playerOffset;

        // fade back in
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, .35f);
        }
        yield return new WaitForSeconds(fadeWaitSeconds);
        // take the player out of the pause state
        playerController.pauseState = false;

    }
}
