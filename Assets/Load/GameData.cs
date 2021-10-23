using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Dictionary<ObjectID, object> LoadedObjects;

    public static void ReloadAssets() {
        Resources.Load("null");
    }
}
