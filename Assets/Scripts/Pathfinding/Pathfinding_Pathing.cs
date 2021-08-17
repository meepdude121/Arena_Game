using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Pathfinding_Pathing : MonoBehaviour
{
    public Transform seeker, target;
    Pathfinding_Grid grid;
    private void Update()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        FindPath(seeker.position, target.position);
        sw.Stop();
        print($"Path found: {sw.ElapsedMilliseconds} ms");
    }
    private void Awake()
    {
        grid = GetComponent<Pathfinding_Grid>();
    }

    public void FindPath(Vector2 StartPos, Vector2 TargetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(StartPos);
        Node targetNode = grid.NodeFromWorldPoint(TargetPos);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node CurrentNode = openSet.RemoveFirst();
            
            closedSet.Add(CurrentNode);

            if (CurrentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(CurrentNode))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbor = CurrentNode.GCost + GetDistance(CurrentNode, neighbour);
                if (newMovementCostToNeighbor < CurrentNode.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCostToNeighbor;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.parentNode = CurrentNode;

                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }
    }
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        path.Reverse();

        grid.path = path;
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY) 
            return 14 * dstY + 10 * (dstX - dstY);
        else
            return 14 * dstX + 10 * (dstY - dstX);
    }
}
