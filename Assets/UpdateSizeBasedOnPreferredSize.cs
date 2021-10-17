using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
[ExecuteAlways]
public class UpdateSizeBasedOnPreferredSize : MonoBehaviour
{
    TextMeshProUGUI TextComponent;
    private void Awake() {
        TextComponent = GetComponent<TextMeshProUGUI>();
    }
    private void OnGUI() {
        (transform as RectTransform).sizeDelta = TextComponent.GetPreferredValues(); 
    }
    // nifty little hack to get code to run in scene view
    private void OnDrawGizmos() {
        (transform as RectTransform).sizeDelta = TextComponent.GetPreferredValues(); 
    }
}
