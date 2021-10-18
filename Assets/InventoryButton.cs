using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Item;
public class InventoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region References
    private Canvas canvas;

    // Button elements
    private CanvasGroup buttonGroup;
    public IItem Contents;
    #endregion
    public void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        Debug.Log(canvas);
        buttonGroup = GetComponent<CanvasGroup>();
        Contents = ItemFactory.GetItem(new ObjectID().StringToItemID("game:testweapon"));
    }
    // todo:
    // allow all stats to be shown on item
    // allow switching between weapons
    // apply this to modes of transport and chassis
    public void OnCreateButton(IItem item) // required to set up new button. Call when instantiating a new prefab
    {
        Contents = ItemFactory.GetItem(item.ItemID);
    }

    #region On Pointer Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.instance.Enable($"<color=#ffffffff>{Contents.Name}</color>\n{Contents.Damage} damage\n{Contents.Weight}g\n{Contents.rarity} {Contents.Type}");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.Disable();
    }
    #endregion
    #region On Click + Hold

    public void OnBeginDrag(PointerEventData eventData)
    {
        buttonGroup.blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        buttonGroup.blocksRaycasts = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        (transform as RectTransform).anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    #endregion
}
