using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 LevelSize;
    public float nodeRadius;
    public Node[,] grid;

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(LevelSize.x, LevelSize.y, 1));
	}
}
