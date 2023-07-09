using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    [SerializeField]
    private GameObject playableSpace;

    private bool centered;
    private List<GameObject> areaSlots;
    private List<Vector3> openCells;
    private List<Vector3> closedCells;

    private GameObject currentCell;
    private GameObject[] neighbors;

    void Start()
    {
        centered = false;
        areaSlots = new List<GameObject>();
        GameObject[] areaSlotsArray = playableSpace.GetComponent<RangeManager>().areaSlots;
        for (int i = 0; i < areaSlotsArray.Length; i++)
        {
            areaSlots.Add(areaSlotsArray[i]);
        }
        openCells = new List<Vector3>();
        closedCells = new List<Vector3>();
    }

    IEnumerator DelayedCenter()
    {
        centered = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = new Vector3(start.position.x, start.position.y, 0);
        currentCell = areaSlots[0];
        openCells.Add(currentCell.transform.position);
        print(currentCell.GetComponent<AreaSlot>().isWall);
        for (int i = 0; i < 8; i++)
        {
            print(FindNeighborsIndex(407)[i]);
        }
    }

    int[] FindNeighborsIndex(int cellIndex)
    {
        int[] cellNeighborsIndex = new int[8];
        for (int i = 0; i < cellNeighborsIndex.Length; i++)
        {
            cellNeighborsIndex[i] = -1;
        }

        int gridSize = areaSlots.Count;
        int gridWidth = 24;

        bool hasTop = cellIndex >= gridWidth;
        bool hasBottom = cellIndex < (gridSize - gridWidth);
        bool hasLeft = (cellIndex % gridWidth) != 0;
        bool hasRight = (cellIndex % gridWidth) != 23;

        if (hasTop && hasLeft)
            cellNeighborsIndex[0] = cellIndex - gridWidth - 1;
        if (hasTop)
            cellNeighborsIndex[1] = cellIndex - gridWidth;
        if (hasTop && hasRight)
            cellNeighborsIndex[2] = cellIndex - gridWidth + 1;
        if (hasLeft)
            cellNeighborsIndex[3] = cellIndex - 1;
        if (hasRight)
            cellNeighborsIndex[4] = cellIndex + 1;
        if (hasBottom && hasLeft)
            cellNeighborsIndex[5] = cellIndex + gridWidth - 1;
        if (hasBottom)
            cellNeighborsIndex[6] = cellIndex + gridWidth;
        if (hasBottom && hasRight)
            cellNeighborsIndex[7] = cellIndex + gridWidth + 1;

        return cellNeighborsIndex;
    }

    Vector3 FindPath()
    {
        Vector3 currentCellPos = currentCell.transform.position;

        if (openCells.Count == 0)
        {
            return currentCellPos;
        }

        if (currentCell.gameObject.GetComponent<AreaSlot>().isStoringItem && currentCell.gameObject.GetComponent<AreaSlot>().dragObject.name == "Goal")
        {
            return currentCellPos;
        }

        closedCells.Add(currentCellPos);
        openCells.Remove(currentCellPos);

        int[] neighborIndices = FindNeighborsIndex(areaSlots.FindIndex(slot => slot.transform.position == currentCellPos));
        List<Vector3> unvisitedNeighbors = new List<Vector3>();
        for (int i = 0; i < neighborIndices.Length; i++)
        {
            int neighborIndex = neighborIndices[i];
            if (neighborIndex != -1)
            {
                Vector3 neighborPos = areaSlots[neighborIndex].transform.position;
                if (!closedCells.Contains(neighborPos) && !openCells.Contains(neighborPos))
                {
                    unvisitedNeighbors.Add(neighborPos);
                }
            }
        }

        if (unvisitedNeighbors.Count == 0)
        {
            Vector3 previousCellPos = closedCells[closedCells.Count - 1];
            return previousCellPos;
        }
        else
        {
            int randomIndex = Random.Range(0, unvisitedNeighbors.Count);
            Vector3 nextCellPos = unvisitedNeighbors[randomIndex];

            currentCell = areaSlots[areaSlots.FindIndex(slot => slot.transform.position == nextCellPos)];
            closedCells.Add(nextCellPos);
            openCells.Remove(nextCellPos);

            return nextCellPos;
        }
    }

    void Update()
    {
        // Centered in update so it properly finds the start cell's position
        if (!centered)
        {
            StartCoroutine(DelayedCenter());
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, FindPath(), 0.1f);
    }
}
