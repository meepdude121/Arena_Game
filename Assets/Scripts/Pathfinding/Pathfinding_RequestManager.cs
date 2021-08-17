using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Pathfinding_RequestManager : MonoBehaviour
{
	public static void RequestAction(PathRequest request)
	{

	}

	struct PathRequest
	{
		Vector2 pathStart;
		Vector2 pathEnd;
		Action<Vector2[], bool> callback;
		PathRequest(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback) 
		{
			this.pathStart = pathStart;
			this.pathEnd = pathEnd;
			this.callback = callback;
		}
	}
}
