using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoomManager : MonoBehaviour
{
    public Grid worldGrid;
    
    [SerializeField]
    private GameObject fadeInPanelPrefab;
    [SerializeField]
    private GameObject fadeOutPanelPrefab;
    [SerializeField]
    private float fadeWaitSeconds = .5f;
    [SerializeField]
    public RoomBuilder roomContainer;

    public FollowPlayer cameraController;
    public TopDownController playerController;
    public GameObject playerObject;
    private Vector3 roomOffset, playerOffset;
    public readonly Dictionary<Vector3, RoomBuilder> roomDictionary = new();
    private readonly float[] noiseSeedArray = new float[4];
    private GameObject fadeInPanel, fadeOutPanel;

    // Panel/transition code from Mister Taft Creates https://www.youtube.com/watch?v=JcEJtEWjiZU

    // Start is called before the first frame update
    public void Start()
    {
        // Assign scene objects
        cameraController = Camera.main.GetComponent<FollowPlayer>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = (TopDownController) playerObject.GetComponent("TopDownController");

        // add first room to the roomList
        
        RoomBuilder firstRoom = Instantiate(roomContainer, this.transform.position, Quaternion.identity);
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
        return roomDictionary[this.transform.position].biome;
    }

    // Starts the transition beteen rooms and begins the coroutine
    public void StartTransition(Vector3 roomPositionOffset, Vector3 playerPositionOffset)
    {
        roomOffset = roomPositionOffset;
        playerOffset = playerPositionOffset;

        // Fade out between rooms
        if(fadeOutPanelPrefab != null)
        {
            fadeOutPanel = Instantiate(fadeOutPanelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        }

        // put the player in a pause state so they can't move
        playerController.pauseState = true;

        StartCoroutine(TransitionCoroutine());
    }

    public void CreateAndAddRoom()
    {
        RoomBuilder newRoom = Instantiate(roomContainer, this.transform.position, Quaternion.identity);
        roomDictionary.Add(this.transform.position, newRoom);
        newRoom.BuildRoom(noiseSeedArray, worldGrid);
        AstarPath.active.Scan();

    }

    public IEnumerator TransitionCoroutine()
    {
        
        yield return new WaitForSeconds(fadeWaitSeconds);

        // move room switchers to next room
        this.transform.position += roomOffset;
        // if the next room isn't in the roomList, make a new RoomBuilder and add it
        if(!roomDictionary.ContainsKey(this.transform.position))
        {
            CreateAndAddRoom();
            AstarPath.active.Scan();
        }
        
        // change camera min/max positions to next room
        cameraController.cameraMinPos += roomOffset;
        cameraController.cameraMaxPos += roomOffset;
        // move player to next room
        playerObject.transform.position += playerOffset;

        // fade back in
        if (fadeInPanelPrefab != null)
        {
            Destroy(fadeOutPanel);
            fadeInPanel = Instantiate(fadeInPanelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(fadeInPanel, .35f);
        }
        yield return new WaitForSeconds(fadeWaitSeconds);
        AstarPath.active.Scan();
        // take the player out of the pause state
        playerController.pauseState = false;

    }
}
