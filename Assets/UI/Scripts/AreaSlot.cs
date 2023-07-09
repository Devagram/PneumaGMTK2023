using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
                var tempHold = GetComponent<Image>();
                Color32 tempColor = new Color32 ();
                tempColor.a = 0;
                tempHold.color = tempColor;
                dragObject = wall;
                isLocked = true;
            }
            if (isNotUsable) 
            {
                dragObject = noUse;

            }
            isChecked = true;
        }
    }
}
