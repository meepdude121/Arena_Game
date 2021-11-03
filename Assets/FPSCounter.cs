using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPSCounter : MonoBehaviour
{
    TextMeshProUGUI textComponent;
    int frameCounter;
    private void Awake() {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateTextEverySecond());
    }

    private void Update() {
        frameCounter++;
    }

    IEnumerator UpdateTextEverySecond(){
        yield return new WaitForSecondsRealtime(1f);
        while (true){
            textComponent.text = $"FPS: {frameCounter}";
            frameCounter = 0;
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
