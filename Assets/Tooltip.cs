using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class Tooltip : MonoBehaviour
{
    // There should always only be one tooltip instance.
    public static Tooltip instance;
    public string Text;

    private TextMeshProUGUI Text_Component;
    private void Awake() {
        instance = this;
        Text_Component = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Update() {
        (transform as RectTransform).anchoredPosition = Mouse.current.position.ReadValue() + new Vector2(Text_Component.preferredWidth / 2f, -Text_Component.preferredHeight / 2f);
    }
    public void Enable(string Text = "No text specified") {
        Text_Component.text = Text;
        (transform as RectTransform).anchoredPosition = Mouse.current.position.ReadValue() + new Vector2(Text_Component.preferredWidth / 2f, -Text_Component.preferredHeight / 2f);

        gameObject.SetActive(true);
    }
    public void Disable() {
        if (gameObject.activeInHierarchy){
            Text_Component.text = "";
            (transform as RectTransform).anchoredPosition = Mouse.current.position.ReadValue() + new Vector2(Text_Component.preferredWidth / 2f, -Text_Component.preferredHeight / 2f);

            gameObject.SetActive(false);
        }
    }
}
