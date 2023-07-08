using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    public float mouseHeldItemLagSpeed = 0.01f;
    public MouseSlot mouseSlot;

    private void Start()
    {
        mouseSlot = GetComponent<MouseSlot>();
        if (mouseSlot == null ) { Debug.Log("child mouseSlot missing"); }

    }

    // Update is called once per frame
    void Update()
    {
        //code used to move item with mouse
        Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        a.Set(a.x, a.y, this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, a, mouseHeldItemLagSpeed);

        if (Input.GetMouseButtonDown(0))
        {
            bool onUI = EventSystem.current.IsPointerOverGameObject();
            if (!onUI) 
            { 
                VacantMouse(mouseSlot.dragObject);
            }
        }
        else 
        {
           // comment here 
        }
    }
    public void VacantMouse(DragableObject heldItem)  // used to release items back to old location. 
    {
        if (heldItem == null)
        { return; }
        else 
        { 
            //droping back into inventory. 
        }
        Debug.Log("MouseController not over ui");
    }

}
