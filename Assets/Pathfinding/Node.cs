using UnityEngine;

public class Node
{
    public bool Walkable;
    public Vector2 WorldPosition;

    public Node(bool Walkable, Vector2 WorldPosition)
	{
        this.Walkable = Walkable;
        this.WorldPosition = WorldPosition;
	}
}
