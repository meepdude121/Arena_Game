using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorMath
{
    public static Vector2 Difference(Vector2 Point, Vector2 Target)
    {
        return Point - Target;
    }

    public static bool ValueWithin(Vector2 Value, float WithinThreshold, Vector2 OfThis)
    {
        return (Value.sqrMagnitude >= OfThis.sqrMagnitude + WithinThreshold && Value.sqrMagnitude <= OfThis.sqrMagnitude - WithinThreshold);
    }
}
