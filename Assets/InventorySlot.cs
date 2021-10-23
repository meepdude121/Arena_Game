using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] public IItem currentItem;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            GameObject objectDropped = eventData.pointerDrag;
            (objectDropped.transform as RectTransform).position = (transform as RectTransform).position;
            currentItem = eventData.pointerDrag.GetComponent<InventoryButton>().Contents;
            Debug.Log(currentItem);
        }
    }
}
