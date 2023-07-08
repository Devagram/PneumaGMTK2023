using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Button button;
    public bool isImageSet = false;
    public MouseController mouseController;
    #region Item data
    public bool isStoringItem = false;
    public DragableObject dragObject;
    public Transform Location;
    #endregion 

    public void Start()
    {
        mouseController = GameObject.FindWithTag("MouseUI").GetComponent<MouseController>();
        if (mouseController == null) { Debug.Log("missing MouseUI tag/ or MouseController component"); }

         button = GetComponent<Button>();
        if (button == null) { Debug.Log("Button component missing"); }
        //
        if (dragObject)
            { isStoringItem = true; }
        else 
            { isStoringItem=false; }
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
        button.image.sprite = dragObject.itemizedSprite;
        isImageSet = true;
    }
    public void ItemDown() 
    {
        button.image.enabled = false;
        // a blank transparent needs added here.   button.image.sprite =
        isImageSet = false;
    }
    public void AttemptMouseExchange()
    {
        if (mouseController.mouseSlot.dragObject != null && dragObject != null) //swaps items
        {
            DragableObject tempStore = dragObject;

            dragObject = mouseController.mouseSlot.dragObject;
            mouseController.mouseSlot.dragObject = tempStore;
            ItemUp();
            mouseController.mouseSlot.ItemUp();


            isImageSet = false;

            mouseController.mouseSlot.isImageSet = false;

            return;
        }
        if (mouseController.mouseSlot.dragObject == null && dragObject != null) //places in mouse
        {
            mouseController.mouseSlot.dragObject = dragObject;
            dragObject = null;
            mouseController.mouseSlot.ItemUp();
            mouseController.mouseSlot.isImageSet = false;

            return;
        }
        if (mouseController.mouseSlot.dragObject != null && dragObject == null) // places in Area
        {
            dragObject = mouseController.mouseSlot.dragObject;
            ItemUp();
            mouseController.mouseSlot.dragObject = null;
            mouseController.mouseSlot.dragObject = null;
            isImageSet = false;

            return;
        }
    }

}
