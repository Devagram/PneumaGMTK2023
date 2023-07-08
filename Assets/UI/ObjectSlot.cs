using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlot : MonoBehaviour
{
    public enum ConditionOperator
    {
        // A field is visible/enabled only if all conditions are true.
        And,
        // A field is visible/enabled if at least ONE condition is true.
        Or,
    }
    public enum ActionOnConditionFail
    {
        // If condition(s) are false, don't draw the field at all.
        DontDraw,
        // If condition(s) are false, just set the field as disabled.
        JustDisable,
    }

    public bool isWall = false;
    public bool isNotUsable = false;

    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.Or, nameof(isWall),
    nameof(isNotUseable))]
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
