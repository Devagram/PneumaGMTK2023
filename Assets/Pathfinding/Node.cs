using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Node : MonoBehaviour 
{
    public List<Node> closeNeighbors = new List<Node>();
    public Transform localTrans;
    public float gCost;
    public float hCost;
    public float fCost;
    public Node cameFromNode;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public float CalculateDistance(Vector3 to, Vector3 from)
    {
        float tempDistance = Vector3.Distance(to, from);
        Debug.Log(tempDistance + "dis");
        return tempDistance;
    }
}

