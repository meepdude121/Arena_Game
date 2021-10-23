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
    public void WhenTextChanged() {
        (transform as RectTransform).sizeDelta = TextComponent.GetPreferredValues(); 
    }
}
