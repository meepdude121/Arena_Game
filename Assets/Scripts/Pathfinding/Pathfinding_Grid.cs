using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Grid : MonoBehaviour
{
	public LayerMask unwalkableMask;
	public Vector2 LevelSize;
	public float nodeRadius;
	public Node[,] grid;

	float nodeDiameter;
	int gridSizeX;
	int gridSizeY;
	private void Start()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(LevelSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(LevelSize.y / nodeDiameter);
		CreateGrid();
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(LevelSize.x, LevelSize.y, 1));
		if (grid != null)
		{
			foreach (Node node in grid)
			{
				Gizmos.color = (node.Walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(node.WorldPosition, new Vector3(1f, 1f, 1f));
			}
		}
	}
	void CreateGrid()
	{
		Debug.Log("Baking pathfind data...");
		Debug.Log($"Creating grid with size {gridSizeX},{gridSizeY}");
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * LevelSize.x / 2 - Vector3.up * LevelSize.y / 2;
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 WorldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				bool Walkable = !Physics.CheckSphere(WorldPoint, nodeRadius - 0.1f, unwalkableMask);
				grid[x, y] = new Node(Walkable, WorldPoint);
			}
		}
	}
}
