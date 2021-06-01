using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for storing information about the enemy. Different enemies also have subclasses that actually contain logic.
/// </summary>
public class Entity : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public float Damage;
    public float DistanceToTarget;
    public int Target;

    public bool AIActive;
    private static float CalculateDistanceToPoint(Vector2 A, Vector2 B) => Mathf.Sqrt((B.x - A.x) * (B.x - A.x) + (B.y - A.y) * (B.y - A.y));
}
