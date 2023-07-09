using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject playableSpace;

    private bool centered;
    private bool isCentering;
    private bool moving;
    private bool won;

    private List<GameObject> cells;
    private List<int> visitedCells;
    private List<int> stackedCells;

    private GameObject currentCell;
    private int currentCellIndex;
    private GameObject targetCell;
    private Vector3 targetPos;
    private int targetCellIndex;

    private int[] neighbors;

    void Start()
    {
        isCentering = false;
        centered = false;
        moving = false;
        won = false;

        cells = new List<GameObject>();
        GameObject[] areaSlotsArray = playableSpace.GetComponent<RangeManager>().areaSlots;
        

        for (int i = 0; i < areaSlotsArray.Length; i++)
        {
            GameObject tempchild = areaSlotsArray[i].transform.GetChild(0).gameObject;
            //
            cells.Add(tempchild);
        }

        visitedCells = new List<int>();
        stackedCells = new List<int>();
    }

    IEnumerator DelayedCenter()
    {
        isCentering = true;
        yield return new WaitForSeconds(1f);

        gameObject.transform.position = new Vector3(start.transform.position.x, start.transform.position.y, 0f);

        currentCell = start;
        currentCellIndex = cells.IndexOf(start);

        centered = true;
    }

    int[] FindWalkableNeighborsIndex(int cellIndex)
    {
        int[] cellWalkableNeighborsIndex = new int[4];
        for (int i = 0; i < cellWalkableNeighborsIndex.Length; i++)
        {
            cellWalkableNeighborsIndex[i] = -1;
        }

        int gridSize = cells.Count;
        int gridWidth = 24;

        bool hasTop = cellIndex >= gridWidth;
        bool hasBottom = cellIndex < (gridSize - gridWidth);
        bool hasLeft = (cellIndex % gridWidth) != 0;
        bool hasRight = (cellIndex % gridWidth) != 23;

        if (hasTop)
            cellWalkableNeighborsIndex[0] = cellIndex - gridWidth;
        if (hasLeft)
            cellWalkableNeighborsIndex[1] = cellIndex - 1;
        if (hasRight)
            cellWalkableNeighborsIndex[2] = cellIndex + 1;
        if (hasBottom)
            cellWalkableNeighborsIndex[3] = cellIndex + gridWidth;

        for (int i = 0; i < cellWalkableNeighborsIndex.Length; i++)
        {
            if (cellWalkableNeighborsIndex[i] != -1 && cells[cellWalkableNeighborsIndex[i]].transform.parent.GetComponent<AreaSlot>().isWall)
            {
                cellWalkableNeighborsIndex[i] = -1;
            }
        }

        return cellWalkableNeighborsIndex;
    }

    GameObject FindPath()
    {
        visitedCells.Add(currentCellIndex);
        stackedCells.Remove(currentCellIndex);

        if (currentCell.transform.parent.GetComponent<Slot>().dragObject.name == "Goal")
        {
            return currentCell;
        }

        List<int> stackedNeighbors = new List<int>();
        neighbors = FindWalkableNeighborsIndex(currentCellIndex);

        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i] != -1 && !visitedCells.Contains(neighbors[i]))
            {
                stackedCells.Add(neighbors[i]);
                stackedNeighbors.Add(neighbors[i]);
            }
        }

        if (stackedNeighbors.Count == 0)
        {
            print("backtrack!");
            int currentIndex = visitedCells.IndexOf(currentCellIndex);
            targetCellIndex = visitedCells[currentIndex - 1];
            return cells[targetCellIndex];
        }

        targetCellIndex = stackedNeighbors[Random.Range(0, stackedNeighbors.Count)];

        return cells[targetCellIndex];
    }

    void Update()
    {
        // Centered in update so it properly finds the start cell's position
        if (!isCentering)
        {
            StartCoroutine(DelayedCenter());
        }
    }

    private void FixedUpdate()
    {
        if (centered && !won)
        {
            if (!moving)
            {
                targetCell = FindPath();
                targetPos = new Vector3(targetCell.transform.position.x, targetCell.transform.position.y, 0f);
                if (currentCell == targetCell)
                {
                    print("You won!");
                    won = true;
                }
            }
            if (gameObject.transform.position != targetPos)
            {
                moving = true;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 0.03f);
            }
            else
            {
                currentCell = targetCell;
                currentCellIndex = cells.IndexOf(currentCell);
                moving = false;
            }
        }
    }
}
