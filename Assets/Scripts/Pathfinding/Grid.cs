using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public LayerMask unWalkableMask;
    public Vector2 gridSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridX;
    int gridY;

    public bool displayGrid = false;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridX, gridY];
        Vector3 bLeft = transform.position - Vector3.right * (gridX / 2) - Vector3.up * (gridY / 2);

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 point = bLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(point, nodeRadius,unWalkableMask));
                grid[x, y] = new Node(walkable, point, x, y);
            }
        }
    }

    public int maxSize
    {
        get
        {
            return gridX * gridY;
        }
    }

    public List<Node> GetSurrounding(Node node)
    {
        List<Node> surrounding = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.x + x;
                int checkY = node.y + y;

                if (checkX >= 0 && checkX < gridX && checkY >= 0 && checkY < gridY)
                {
                    surrounding.Add(grid[checkX, checkY]);
                }

            }
        }
        return surrounding;
    }

    public Node NodeFromPoint(Vector3 wPos)
    {
        float percentX = (wPos.x + gridSize.x / 2) / gridSize.x;
        float percentY = (wPos.y + gridSize.y / 2) / gridSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridX - 1) * percentX);
        int y = Mathf.RoundToInt((gridY - 1) * percentY);

        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));
        if (grid != null && displayGrid)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
