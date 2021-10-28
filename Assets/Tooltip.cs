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
    public Vector2 offset;
    public string Text;
    public CanvasGroup canvasGroup;
    private Canvas parentCanvas;

    private TextMeshProUGUI Text_Component;
    private void Awake() {
        instance = this;
        Text_Component = GetComponentInChildren<TextMeshProUGUI>();
        parentCanvas = transform.parent.GetComponent<Canvas>();
    }
    public void Update() {
        (transform as RectTransform).anchoredPosition = (Mouse.current.position.ReadValue() + new Vector2((Text_Component.preferredWidth / 2f) + offset.x, (-Text_Component.preferredHeight / 2f) + offset.y)) / parentCanvas.scaleFactor;
    }
    // set text
    public void Enable(string Text = "No text specified") {
        
        // change text
        Text_Component.text = Text;

        // set the size 
        (Text_Component.transform as RectTransform).sizeDelta = Text_Component.GetPreferredValues();

        // set position to the mouse position + offset, multiply position by scale of canvas to fix position on high resolution screens
        (transform as RectTransform).anchoredPosition = (Mouse.current.position.ReadValue() + new Vector2((Text_Component.preferredWidth / 2f) + offset.x, (-Text_Component.preferredHeight / 2f) + offset.y)) / parentCanvas.scaleFactor;

        canvasGroup.alpha = 1;
    }
    public void Disable() {

        // could change this to a regular canvas and disable that to remove OnGUI calls every time the mouse is moved
        // instead of setting alpha, but I am unsure as to whether unity bothers drawing CanvasGroups with alpha of 0

        // if the object hasn't already been disabled
        if (canvasGroup.alpha != 0) {
            // set alpha to 0
            canvasGroup.alpha = 0;
            // remove text
            Text_Component.text = "";
        }
    }
}
