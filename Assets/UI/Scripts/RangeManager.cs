using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    public GameObject[] areaSlots;
    public float rangeMultAdjuster = 16; // not sure if this is pizels or units


    private void Awake()
    {
        areaSlots = GameObject.FindGameObjectsWithTag("AreaUI");
        if (areaSlots == null) 
        { Debug.Log("tag missing AreaUI"); }
        foreach (GameObject slot in areaSlots) 
        {
            AreaSlot checkComp = slot.GetComponent<AreaSlot>();
            if (checkComp == null) 
            {
                Debug.Log("missing Compoent AreaSlot on tagged AreaUI");
            }
        }
    }
    // lets you know if you can place an item that requires range offset.
    public bool CheckRange(int rangeAmount, Transform location)   // send the range amount of the drag item and the location of the tile you want to use.
    {
        bool isClear = true;

        foreach (GameObject slot in areaSlots)
        {
            float currentDistance;
            AreaSlot checkComp = slot.GetComponent<AreaSlot>();
            if (checkComp != null)
            {
                //Debug.Log("comp found");
                if (checkComp.dragObject != null && checkComp.dragObject.name != "Empty")
                {
                    //Debug.Log("Object is present");
                    int othersRange = checkComp.dragObject.rangedisable;
                    currentDistance = Vector3.Distance(slot.transform.position, location.position);
                    
                    if (currentDistance <= rangeAmount * rangeMultAdjuster || currentDistance <= othersRange * rangeMultAdjuster)
                    {
                        //Debug.Log("slot in range for object in mouse");

                        isClear = false;
                    }

                }
            } 
        }
        return isClear;
    }   
}
