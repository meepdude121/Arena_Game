using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, Range(0f, 2f), Tooltip("The amount of time the player needs to hold down the mouse button to confirm the upgrade.\nIncrease for more expensive upgrades.")]
    public float TimeToHold;
    [SerializeField, Tooltip("The text to display when the player is hovering over the button")]
    public string ToolTip;
    [SerializeField, Tooltip("The UpgradeObject this button activates")]
    public UpgradeObject upgradeObject;
    
    private float HoldTime;
    private void UpdateHoldTime() => HoldTime += Time.deltaTime;
    private void ResetHoldTime() => HoldTime = 0f;
    private void ActivateToolTip() => Tooltip.instance.Enable(ToolTip);
    private void DeactivateToolTip() => Tooltip.instance.Disable();

    #region Input
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
        ActivateToolTip();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
        DeactivateToolTip();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
        UpdateHoldTime();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
        ResetHoldTime();
    }
    #endregion
}

public class UpgradeObject : ScriptableObject 
{

}
