using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pathFindering : MonoBehaviour
{
    public List<Transform> destinations = new List<Transform>(); //small path storage
    public List<Node> areaBank = new List<Node>();
    public int wanderRange = 3;// distance before re-wanderPath
    public int senceDistance = 5; // distance to engage chest
    public int stepToExit = 2; // amount of wander paths before direct exitpath
    private List<Node> openNodes;
    private List<Node> closedNodes;

    private void Start() //done  may want to call this right before stage changes hero movement so it is most up to date . start is for testing. 
    {
        GameObject[] areaSlots = GameObject.FindGameObjectsWithTag("AreaUI");
        if (areaSlots == null)
        { Debug.Log("tag missing AreaUI"); }
        foreach (GameObject slot in areaSlots)
        {
            AreaSlot checkComp = slot.GetComponent<AreaSlot>();
            if (checkComp == null)
            {
                Debug.Log("missing Compoent AreaSlot on tagged AreaUI");
            }
            else if (!checkComp.isWall) // is it a wall ignore it
            {
                Node newNode = new Node(); ;
                newNode.localTrans = slot.transform; /// there maybe be an offsset +-8 on x and y axis in tansforms. 
                areaBank.Add(newNode);
            }
        }
    }
    public Node FindClosestTransform(GameObject requestingHero) //done
    {
        Node leastDistance = null;
        float currentMinimum = 1000;
        foreach (Node item in areaBank)
        {
            float distanceOfSlot = Vector3.Distance(requestingHero.transform.position, item.localTrans.position);
            if (distanceOfSlot <= currentMinimum)
            {
                currentMinimum = distanceOfSlot;
                leastDistance = item;
            }
        }
        return leastDistance;
    }
    public Transform WanderPath(GameObject requestingHero, Transform closestSlotToCurrentLocation) // needs to call the path finder function //also needs to return list
    { 
        Transform tempPath = null;
        //picks random direction and pathfinds to location
        int index = Random.Range( 0,areaBank.Count);
        float distanceOfSlot = Vector3.Distance(closestSlotToCurrentLocation.position, areaBank[index].localTrans.position);
        if (distanceOfSlot <= wanderRange)
        {
            tempPath = areaBank[index].localTrans;
        }

        return tempPath;
    }
    public List<Transform> SencePath(GameObject requestingHero, Transform closestSlotToCurrentLocation) // needs to call the path finder function
    {
        List<Transform> tempPath = new List<Transform>();
        //pick direction toward chest and pathfinds to it 
        return destinations;
    }
    public List<Transform> ExitPath(GameObject requestingHero, Transform closestSlotToCurrentLocation) // needs to call the path finder function //also needs to return list
    {
        List<Transform> tempPath = new List<Transform>();
        //picks direction toward nearest exit and pathfinds to it

        return destinations;
    }

    public List<Node> FindPathToDestination(Node startNode, Node desiredNode, GameObject requestingHero)
    {
        openNodes = new List<Node> { startNode };
        closedNodes = new List<Node>();
        for (int i = 0; i < areaBank.Count; i++)
        {
            areaBank[i].gCost = 1000f;
            areaBank[i].CalculateFCost(); // need to simplify a function here
            areaBank[i].cameFromNode = null;
        }
        startNode.gCost = 0f;
        startNode.hCost = startNode.CalculateDistance(startNode.transform.position, desiredNode.transform.position);
        startNode.CalculateFCost();

        //the loop

        while (openNodes.Count > 0)
        {
            Node thisGridPoint = GetLowestFCostNode(openNodes); // is this the right list to check.. sould it be the total list?
            if (thisGridPoint == desiredNode)
            {
                return CalculatedPath(desiredNode);
            }
            openNodes.Remove(thisGridPoint);
            closedNodes.Add(thisGridPoint);  //should this be happening. 
            foreach (Node neighbor in thisGridPoint.closeNeighbors)
            {
                float tempDistance = neighbor.CalculateDistance(neighbor.transform.position, thisGridPoint.transform.position);
                float tentativeGCost = thisGridPoint.gCost + tempDistance;
                if (tentativeGCost < neighbor.gCost)
                {
                    neighbor.cameFromNode = thisGridPoint;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = neighbor.CalculateDistance(neighbor.transform.position, desiredNode.transform.position);
                    neighbor.CalculateFCost();
                    if (!openNodes.Contains(neighbor))
                    {
                        openNodes.Add(neighbor);
                    }
                }
            }
        }
        Debug.Log("no paths found");
        return null;
    }
    public Node GetLowestFCostNode(List<Node> openNodes) 
    {
        return null;// needs filled in 
    }
    public List<Node> CalculatedPath(Node lastNode) 
    {
        return null; // needs filled
    }

}
