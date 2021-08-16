using System;
using UnityEngine;

public class Node : IHeapItem<Node>
{
	public bool Walkable;
	public Vector2 WorldPosition;
	public int GridX;
	public int GridY;

	public int GCost;
	public int HCost;

	public Node parentNode;
	int heapIndex;
	public Node(bool Walkable, Vector2 WorldPosition, int GridX, int GridY)
	{
		this.Walkable = Walkable;
		this.WorldPosition = WorldPosition;
		this.GridX = GridX;
		this.GridY = GridY;
	}
	public int FCost 
	{ 
		get
        {
			return GCost + HCost;
        }
	}

    public int HeapIndex
    {
		get
        {
			return heapIndex;
        }
        set
        {
			heapIndex = value;
        }
    }

	public int CompareTo(Node nodeToCompare)
    {
		int compare = FCost.CompareTo(nodeToCompare.FCost);
		if (compare == 0)
        {
			compare = HCost.CompareTo(nodeToCompare.HCost);
        }
		return -compare;
    }
}
