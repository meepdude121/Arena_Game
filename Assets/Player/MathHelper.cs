using UnityEngine;

public class MathHelper
{
    public static float CalculateDistanceBetweenPoints(Vector2 A, Vector2 B) => Mathf.Sqrt((B.x - A.x) * (B.x - A.x) + (B.y - A.y) * (B.y - A.y));
}
