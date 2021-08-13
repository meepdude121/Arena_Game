using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityMethods
{
	public static bool IsWithinBoundsOf(this float number, float checkNumber, float Bounds) => number >= checkNumber - Bounds && number <= checkNumber + Bounds;
}
