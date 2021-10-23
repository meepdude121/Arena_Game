using UnityEngine;

public class Startup : MonoBehaviour
{
    private void Awake() {
        GameData.ReloadAssets();
    }
}
