using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TopDownController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = new TopDownController("Squirtle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
