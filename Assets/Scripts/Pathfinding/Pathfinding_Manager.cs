using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinding_Manager : MonoBehaviour
{

    Pathfinding_Grid grid;
    static Pathfinding_Manager instance;

    void Awake()
    {
        grid = GetComponent<Pathfinding_Grid>();
        instance = this;
    }

    public static Vector2[] RequestPath(Vector2 from, Vector2 to)
    {
        return instance.FindPath(from, to);
    }

    Vector2[] FindPath(Vector2 from, Vector2 to)
    {

        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector2[] waypoints = new Vector2[0];
        bool pathSuccess = false;

        Pathfinding_Node startNode = grid.NodeFromWorldPoint(from);
        Pathfinding_Node targetNode = grid.NodeFromWorldPoint(to);
        startNode.parent = startNode;

        if (!startNode.walkable)
        {
            startNode = grid.ClosestWalkableNode(startNode);
        }
        if (!targetNode.walkable)
        {
            targetNode = grid.ClosestWalkableNode(targetNode);
        }

        if (startNode.walkable && targetNode.walkable)
        {

            Heap<Pathfinding_Node> openSet = new Heap<Pathfinding_Node>(grid.MaxSize);
            HashSet<Pathfinding_Node> closedSet = new HashSet<Pathfinding_Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Pathfinding_Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    pathSuccess = true;
                    break;
                }

                foreach (Pathfinding_Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + TurningCost(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }
        }

        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }

        return waypoints;

    }


    int TurningCost(Pathfinding_Node from, Pathfinding_Node to)
    {
        /*
		Vector2 dirOld = new Vector2(from.gridX - from.parent.gridX, from.gridY - from.parent.gridY);
		Vector2 dirNew = new Vector2(to.gridX - from.gridX, to.gridY - from.gridY);
		if (dirNew == dirOld)
			return 0;
		else if (dirOld.x != 0 && dirOld.y != 0 && dirNew.x != 0 && dirNew.y != 0) {
			return 5;
		}
		else {
			return 10;
		}
		*/

        return 0;
    }

    Vector2[] RetracePath(Pathfinding_Node startNode, Pathfinding_Node endNode)
    {
        List<Pathfinding_Node> path = new List<Pathfinding_Node>();
        Pathfinding_Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        //Vector2[] waypoints = SimplifyPath(path);
        Vector2[] waypoints = new Vector2[path.Count];
        // convert path (nodes) to points (Vector2)
        for(int i = 0; i < path.Count; i++)
        {
            waypoints[i] = path[i].worldPosition;
        }
        // Reverse array so points are from start position to end position ordering
        Array.Reverse(waypoints);
        // Remove first point to avoid strange jitter behaviour
        Vector2[] oldWaypoints = waypoints;
        if (waypoints.Length - 1 < 0)
        {
            return new Vector2[0];
        }
        waypoints = new Vector2[waypoints.Length - 1];
        for (int i = 1; i < path.Count; i++)
        {
            waypoints[i-1] = oldWaypoints[i];
        }
        return waypoints;

    }

    Vector2[] SimplifyPath(List<Pathfinding_Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Pathfinding_Node nodeA, Pathfinding_Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }


}
