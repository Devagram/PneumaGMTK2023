using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Button button;
    public bool isImageSet = false;
    public MouseController mouseController;
    protected bool isLocked = false;
    public RangeManager rangeManager;
    #region Item data
    public bool isStoringItem = false;
    public DragableObject dragObject;
    public Transform Location;
    public bool containsItem;
    #endregion 

    public void Start()
    {
        rangeManager = GameObject.FindWithTag("RangeUI").GetComponent<RangeManager>();
        if (rangeManager == null) { Debug.Log("missing RangeUI tag/ or RangeManager component"); }

        mouseController = GameObject.FindWithTag("MouseUI").GetComponent<MouseController>();
        if (mouseController == null) { Debug.Log("missing MouseUI tag/ or MouseController component"); }

         button = GetComponent<Button>();
        if (button == null) { Debug.Log("Button component missing"); }
        //
        if (dragObject)
            { isStoringItem = true; containsItem = true; }
        else 
            { isStoringItem = false; containsItem = false; }
    }
    void Update()
    {
        if (!isImageSet && isStoringItem == true)
            {
            ItemUp();
            }

        if (isStoringItem == false) 
        {
            ItemDown();
        }

    }
    public void ItemUp()
    {
        button.image.enabled = true;
        containsItem = true;
        button.image.sprite = dragObject.itemizedSprite;
        isImageSet = true;
    }
    public void ItemDown() 
    {
        button.image.enabled = false;
        //containsItem = true;
        gameObject.name = "EmptySlot";
        // a blank transparent needs added here.   button.image.sprite =
        isImageSet = false;
    }
    public void AttemptMouseExchange()
    {

        if (isLocked)
        { return; }
        
        if (mouseController.mouseSlot.dragObject != null && mouseController.mouseSlot.dragObject.name != "Empty" ) 
        {
            if (!rangeManager.CheckRange(mouseController.mouseSlot.dragObject.rangedisable, transform)) 
            { return; }
        }
        if (mouseController.mouseSlot.dragObject != null && (dragObject != null || dragObject.name != "Empty")) //swaps items
        {
            containsItem = false;
            DragableObject tempStore = dragObject;

            dragObject = mouseController.mouseSlot.dragObject;
            mouseController.mouseSlot.dragObject = tempStore;
            ItemUp();
            mouseController.mouseSlot.ItemUp();


            isImageSet = false;

            mouseController.mouseSlot.isImageSet = false;

            return;
        }
        if ((mouseController.mouseSlot.dragObject == null || mouseController.mouseSlot.dragObject.name == "Empty") && dragObject != null) //places in mouse
        {
            mouseController.mouseSlot.dragObject = dragObject;
            dragObject = null;
            mouseController.mouseSlot.ItemUp();
            containsItem = false;
            mouseController.mouseSlot.isImageSet = false;

            return;
        }
        if (mouseController.mouseSlot.dragObject != null && (dragObject == null || mouseController.mouseSlot.dragObject.name == "Empty"))// places in slot
        {
            dragObject = mouseController.mouseSlot.dragObject;
            ItemUp();
            containsItem = true;
            mouseController.mouseSlot.dragObject = null;
            mouseController.mouseSlot.dragObject = null;
            isImageSet = false;

            return;
        } 

    }

}
