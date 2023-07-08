using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AreaSlot : Slot
{
    public bool isWall = false;
    public bool isNotUsable = false;

    private bool isChecked = false;
    public DragableObject wall;
    public DragableObject noUse;

    public void Awake()
    {
        if (!isChecked)
        { 
            if (isWall) 
            {
                dragObject = wall;
                isLocked = true;
            }
            if (isNotUsable) 
            {
                dragObject = noUse;
                isLocked = true;
            }
            isChecked = true;
        }
    }
}
