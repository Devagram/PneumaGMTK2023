using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PathFindering : MonoBehaviour
{
    public List<Transform> destinations = new List<Transform>(); //small path storage
    public List<Node> areaBank = new List<Node>();
    public int wanderRange = 16;// distance before re-wanderPath
    public int senceDistance = 5; // distance to engage chest
    public int stepToExit = 2; // amount of wander paths before direct exitpath
    private List<Node> openNodes;
    private List<Node> closedNodes;
    public GameObject nodePrefab;

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
                checkComp.PopulateNeighbors();// this needs checked{nessecity. }
                GameObject newNode = Instantiate(nodePrefab, gameObject.transform, checkComp.Location);
                Node nodeNet = newNode.GetComponent<Node>();
                nodeNet.localTrans.position = checkComp.Location.position;
                nodeNet.transform.position = checkComp.transform.position;/// there maybe be an offsset +-8 on x and y axis in tansforms. 
                areaBank.Add(nodeNet);
            }
        }
    }
    public Node FindClosestTransformNode(GameObject requestingHero) //done
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
    public List<Node> WanderPath(GameObject requestingHero, Node closestSlotToCurrentLocation) // needs to call the path finder function //also needs to return list
    { 
        Node destinationNode = null;
        //picks random direction and pathfinds to location
        int index = Random.Range( 0,areaBank.Count);
        float distanceOfSlot = Vector3.Distance(closestSlotToCurrentLocation.localTrans.position, areaBank[index].localTrans.position);
        if (distanceOfSlot <= wanderRange)
        {
            destinationNode = areaBank[index];
        }
        return FindPathToDestination(closestSlotToCurrentLocation,destinationNode);

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

    public List<Node> FindPathToDestination(Node startNode, Node desiredNode)
    {
        openNodes = new List<Node> { startNode };
        closedNodes = new List<Node>();
        for (int i = 0; i < areaBank.Count; i++)
        {
            areaBank[i].gCost = 1000f;
            areaBank[i].CalculateFCost();
            areaBank[i].cameFromNode = null;
        }
        startNode.gCost = 0f;
        startNode.hCost = startNode.CalculateDistance(startNode.localTrans.position, desiredNode.localTrans.position);
        
        startNode.CalculateFCost();

        //the loop

        while (openNodes.Count > 0)
        {
            Node newTempNode = GetLowestFCostNode(openNodes);
            if (newTempNode == desiredNode)
            {
                return CalculatedPath(desiredNode);
                Debug.Log("check");
            }
            openNodes.Remove(newTempNode);
            closedNodes.Add(newTempNode);
            foreach (Node neighbor in newTempNode.closeNeighbors)
            {
                float tempDistance = neighbor.CalculateDistance(neighbor.localTrans.position, newTempNode.localTrans.position);
                float tentativeGCost = newTempNode.gCost + tempDistance;
                if (tentativeGCost < neighbor.gCost)
                {
                    neighbor.cameFromNode = newTempNode;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = neighbor.CalculateDistance(neighbor.localTrans.position, desiredNode.localTrans.position);
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
        Node lowestCostNode = openNodes[0];
        for (int i = 0; i < openNodes.Count; i++)
        {
            if (openNodes[i].fCost < lowestCostNode.fCost)
            {
                lowestCostNode = openNodes[i];
            }
        }
        return lowestCostNode;
    }
    public List<Node> CalculatedPath(Node lastNode) 
    {
        bool isLast = true;
        List<Node> path = new List<Node>();
        path.Add(lastNode);
        Node currentGridPoint = lastNode;
        while (currentGridPoint.cameFromNode != null)
        {
            path.Add(currentGridPoint.cameFromNode);
            if (isLast == true)
            {
                isLast = false;
            }
            currentGridPoint = currentGridPoint.cameFromNode;
        }
        path.Reverse();
        return path;
    }

}
