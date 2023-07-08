using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private DragableObject heldObject = null;
    private bool isOverItem = false;
    private bool isOverSlot = false;
    private bool isOverNull = true; // intial state used is you dont want to drop an item. any unplacable area. 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {

            }
        }
    }
    private void OnMouseEnter()
    {
       

    }
    public void MoveItemToMouse(DragableObject helditem) 
    {
        //helditem.Location= Input.mousePosition;  // conversion needed
    }

    private bool itemCheck() 
        { return true; }
}
