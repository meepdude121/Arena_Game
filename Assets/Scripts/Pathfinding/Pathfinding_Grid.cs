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
		nodeDiameter = nodeRadius;
		gridSizeX = Mathf.RoundToInt(LevelSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(LevelSize.y / nodeDiameter);
		CreateGrid();
	}
	public List<Node> path;
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(LevelSize.x, LevelSize.y, 1));
		if (grid != null)
		{
			foreach (Node node in grid)
			{
				if (node != null)
                {
					Gizmos.color = node.Walkable ? new Color(1, 1, 1, 0f) : new Color(1, 0, 0, .25f);
					if (path != null)
					{
						if (path.Contains(node))
						{
							Gizmos.color = new Color(0f, 1f, 0.5f, .25f);
						}
					}
					Gizmos.DrawWireCube(node.WorldPosition, new Vector3(1f, 1f, 0f));
					Gizmos.color = node.Walkable ? new Color(1, 1, 1, 0f) : new Color(1, 0, 0, .25f);
					if (path != null)
					{
						if (path.Contains(node))
						{
							Gizmos.color = new Color(0f, 1f, 0.5f, 1f);
						}
					}
					Gizmos.DrawCube(node.WorldPosition, new Vector3(1f, 1f, 0f));
				}

			}
		}
	}
	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * LevelSize.x / 2 - Vector3.up * LevelSize.y / 2;
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 WorldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				bool Walkable = !Physics.CheckSphere(WorldPoint, nodeRadius - 0.8f, unwalkableMask);
				grid[x, y] = new Node(Walkable, WorldPoint, x, y);

			}
		}
	}
	public List<Node> GetNeighbours(Node node)
    {
		List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
				if (x == 0 && y == 0) continue;

				int checkX = node.GridX + x;
				int checkY = node.GridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
					neighbours.Add(grid[checkX, checkY]);
                }
			}
        }
		return neighbours;
    }
	public Node NodeFromWorldPoint(Vector2 WorldPosition)
    {
		// Get world space to percent of grid
		// Return adjusted percent:
		//   (a + b/2) / b
		// Optimize for performance:
		// = (a/b) + (b(2*b))
		// = a/b + 1/2
		// = a/b + 0.5f
		// a/b + 0.5f is cheaper to compute than (a+b/2)/b
		float PercentX = (WorldPosition.x / LevelSize.x) + 0.5f;
		float PercentY = (WorldPosition.y / LevelSize.y) + 0.5f;
		//float PercentX = (WorldPosition.x + LevelSize.x / 2) / LevelSize.x;
		//float PercentY = (WorldPosition.y + LevelSize.y / 2) / LevelSize.y;
		PercentX = Mathf.Clamp01(PercentX);
		PercentY = Mathf.Clamp01(PercentY);

		int x = Mathf.RoundToInt((gridSizeX - 1f) * PercentX);
		int y = Mathf.RoundToInt((gridSizeY - 1f) * PercentY);
		return grid[x, y];
	}
	public int MaxSize
    {
		get
        {
			return gridSizeX * gridSizeY;
        }
    }
}
