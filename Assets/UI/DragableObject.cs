using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    public bool isMovableByPlayer = false; // used for lvls with addtional walls and during game loop
    public bool isHeldByMouse = false;
    public Sprite itemizedSprite; // appearance when held by mouse
    public Transform Location;  //where is this item. 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
