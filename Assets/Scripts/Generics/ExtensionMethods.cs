using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static bool CompareLayer(this GameObject gameObject, int Layer) => gameObject.layer == Layer;
}
