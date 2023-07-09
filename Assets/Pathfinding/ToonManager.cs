using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonManager : MonoBehaviour
{
    public List<Node> destList = new List<Node>();
    public bool isPaused = false;
    public bool seesChest = false; // changes path prediction vv
    public bool hasGold = false;  // set to send to room exit ~~
    public PathFindering pathfinder;
    public Node currentNode;
    private void Start()
    {
        pathfinder = GameObject.FindWithTag("PathAI").GetComponent<PathFindering>();
        if (pathfinder == null) { Debug.Log("missing PathAI tag/ or PathFindering component"); isPaused = true; }
    }

    private void Update()
    {
        if (isPaused) { return; }

        if (!seesChest && !hasGold) 
        { 
            if (destList.Count < 1) 
            {
                currentNode = pathfinder.FindClosestTransformNode(this.gameObject);
                if (currentNode == null ) 
                { 
                    Debug.Log("Toonmanager not able to find Node for pathfinding"); 
                }
                else 
                { 
                    destList  = pathfinder.WanderPath(this.gameObject, currentNode);
                }
            }
            else 
            { 
            isPaused=false;
            }
        }

    }

}
