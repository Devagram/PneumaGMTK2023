using System.Collections.Generic;
//using System.Linq;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node : MonoBehaviour 
{
    public List<Node> closeNeighbors = new List<Node>();
    public Transform localTrans;
    public float gCost;
    public float hCost;
    public Node cameFromNode;

    public void CalculateFCost() { }
    public float CalculateDistance(Vector3 to, Vector3 From)
    {
        return .0f; // needs filled
    }

}

