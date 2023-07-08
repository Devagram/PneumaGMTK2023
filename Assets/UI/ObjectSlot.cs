using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSlot : MonoBehaviour
{
    public bool isWall = false;
    public bool isNotUsable = false;
    public DragableObject dragObject;
    public Sprite displaySprite;  // image used in runtime  //this may need to be changed to support animation. 
    public Transform Location;

    void Start()
    {
        //on break

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
