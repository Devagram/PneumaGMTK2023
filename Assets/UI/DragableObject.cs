
using UnityEngine;

[CreateAssetMenu(fileName = "dragableMenuItem", menuName = "ScriptableObjects/Item")]
public class DragableObject : ScriptableObject
{
    public bool isHeldByMouse = false;
    public bool isInInventroy = false;
    public bool isInPlayArea = false;
    public Sprite itemizedSprite; // appearance when held by mouse
}
