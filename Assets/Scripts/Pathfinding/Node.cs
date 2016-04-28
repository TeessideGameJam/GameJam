using UnityEngine;
using System.Collections;

public class Node : IHItem<Node>
{
    public bool walkable;
    public Vector3 worldPos;
    public int x;
    public int y;

    public int g;
    public int h;
    int hIndex;

    public Node parent;


    public Node(bool walk, Vector3 wPos, int gX, int gY)
    {
        walkable = walk;
        worldPos = wPos;
        x = gX;
        y = gY;
    }

    public int f
    {
        get {
            return g + h;
        }
    }

    public int HIndex
    {
        get
        {
            return hIndex;
        }
        set
        {
            hIndex = value;
        }
    }

    public int CompareTo(Node compNode)
    {
        int compare = f.CompareTo(compNode.f);
        if (compare == 0)
        {
            compare = h.CompareTo(compNode.h);
        }
        return -compare;
    }
}
