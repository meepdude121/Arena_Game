using UnityEngine;

public class OkSeriouslyThoughHowLaggyWillThisBeRealistically : MonoBehaviour
{
    public int Iterations = 10;
    GameObject defaultTemplate;
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    private void Awake() {
        stopwatch.Start();
        defaultTemplate = Resources.Load<GameObject>("GUIAssets/Inventory/ItemButtons/Fallback");
        for (int i = 0; i < Iterations; i++){
            Instantiate(defaultTemplate, transform);
        }
        stopwatch.Stop();

        Debug.Log(stopwatch.ElapsedMilliseconds);
    }
}
