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
    public List<AreaSlot> closeNeighbors ;
    public float distanceToNieghborMax = 0.4f;
    public float distanceToNieghborMmin = 0.4f;

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

            }
            isChecked = true;
        }
    }
    public void PopulateNeighbors()
    {
        GameObject[] tempGPArray = GameObject.FindGameObjectsWithTag("AreaUI");
        closeNeighbors = new List<AreaSlot>();
        foreach (GameObject GP in tempGPArray)
        {
            float tempDistance = Vector3.Distance(GP.transform.position, transform.position);
            if (tempDistance <= 2.5f && tempDistance > 1)// distance needs resolved
            {

                if (GP != null)
                {
                    AreaSlot tempGridPoint = GP.GetComponent<AreaSlot>();
                    if (!tempGridPoint.isWall)
                    {
                        closeNeighbors.Add(tempGridPoint);
                    }
                }
            }
        }
    }
}
