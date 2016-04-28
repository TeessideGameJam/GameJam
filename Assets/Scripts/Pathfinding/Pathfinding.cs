using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour
{

    PathManager pManager;
    Grid grid;
    private const int STRAIGHT = 10;
    private const int DIAGONAL = 14;

    void Awake()
    {
        grid = GetComponent<Grid>();
        pManager = GetComponent<PathManager>();
    }

    public void StartPathFind(Vector3 startPos, Vector3 endPos)
    {
        StartCoroutine(FindPath(startPos, endPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 target)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromPoint(startPos);
        Node endNode = grid.NodeFromPoint(target);

        if (startNode.walkable && endNode.walkable)
        {
            Heap<Node> open = new Heap<Node>(grid.maxSize);
            HashSet<Node> closed = new HashSet<Node>();
            open.Add(startNode);

            while (open.Count > 0)
            {
                Node cNode = open.RemoveFirst();
                closed.Add(cNode);

                if (cNode == endNode)
                {
                    sw.Stop();
                    print("Path Found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    break;
                }

                foreach (Node touchingNode in grid.GetSurrounding(cNode))
                {
                    if (!touchingNode.walkable || closed.Contains(touchingNode))
                    {
                        continue;
                    }

                    int newCost = cNode.g + GetDistance(cNode, touchingNode);

                    if (newCost < cNode.g || !open.Contains(touchingNode))
                    {
                        touchingNode.g = newCost;
                        touchingNode.h = GetDistance(touchingNode, endNode);
                        touchingNode.parent = cNode;

                        if (!open.Contains(touchingNode))
                        {
                            open.Add(touchingNode);
                        }
                        else
                        {
                            open.ItemUpdate(touchingNode);
                        }
                    }
                }
            }
            yield return null;
            if (pathSuccess)
            {
                waypoints = PathTrace(startNode, endNode);
            }
            pManager.PathProcessFinished(waypoints, pathSuccess);
        }
    }

    int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.x - b.x);
        int distY = Mathf.Abs(a.y - b.y);

        if (distX > distY)
        {
            return DIAGONAL * distY + STRAIGHT * (distX - distY);
        }
        return DIAGONAL * distX + STRAIGHT * (distY - distX);
    }

    Vector3[] PathTrace(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node cNode = end;

        while (cNode != start)
        {
            path.Add(cNode);
            cNode = cNode.parent;
        }
        Vector3[] waypoints = PathSimplify(path);
        Array.Reverse(waypoints);
        return waypoints;
        
    }

    Vector3[] PathSimplify(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 oldDir = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDir = new Vector2(path[i - 1].x - path[i].x, path[i - 1].y - path[i].y);
            if (newDir != oldDir)
            {
                waypoints.Add(path[i].worldPos);
            }
            oldDir = newDir;
        }
        return waypoints.ToArray();
    }
}
