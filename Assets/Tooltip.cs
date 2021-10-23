using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    // There should always only be one tooltip instance.
    public static Tooltip instance;
    public string Text;
    public CanvasGroup canvasGroup;

    private TextMeshProUGUI Text_Component;
    private void Awake() {
        instance = this;
        Text_Component = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Update() {
        (transform as RectTransform).anchoredPosition = Mouse.current.position.ReadValue() + new Vector2((Text_Component.preferredWidth / 2f) + 15f, (-Text_Component.preferredHeight / 2f) - 15f);
    }
    public void Enable(string Text = "No text specified") {
        Text_Component.text = Text;
        Debug.Log(Text_Component.text);
        (Text_Component.transform as RectTransform).sizeDelta = Text_Component.GetPreferredValues();
        (transform as RectTransform).anchoredPosition = Mouse.current.position.ReadValue() + new Vector2((Text_Component.preferredWidth / 2f) + 15f, (-Text_Component.preferredHeight / 2f) + 15f);

        canvasGroup.alpha = 1;
    }
    public void Disable() {
        if (canvasGroup.alpha != 0) {
            Text_Component.text = "";
            canvasGroup.alpha = 0;
        }
    }
}
