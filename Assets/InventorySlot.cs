using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public IItem currentItem;
    public bool containsItem;

    // when an object has been dropped onto this slot
    public void OnDrop(PointerEventData eventData)
    {
        // check whether the object exists, and if it does check whether it matches the tag InventoryItem
        // if it never checked it random ui elements would be able to be dragged and dropped onto the slot
        if (eventData.pointerDrag && eventData.pointerDrag.CompareTag("InventoryItem"))
        {
            // if there isn't an item in the slot
            if (!containsItem) {

                // store the dropped GameObject temporarily
                GameObject objectDropped = eventData.pointerDrag;

                // snap the GameObject to the position of the slot
                (objectDropped.transform as RectTransform).position = (transform as RectTransform).position;
            
                // set the parent so UI masking works
                objectDropped.transform.SetParent(transform);

                // set the currentItem field to the contents of the inventory button
                // which can be used in slots that use the active item
                currentItem = eventData.pointerDrag.GetComponent<InventoryButton>().Contents;

                containsItem = true;
            } 
            // otherwise swap the items in the slots
            // this is more expensive because there are more things to change
            else 
            {
                // store other inventory slot and its component
                GameObject otherSlot = eventData.pointerDrag.transform.parent.gameObject;
                InventorySlot otherSlotComponent = otherSlot.GetComponent<InventorySlot>();

                // store the currently stored object
                GameObject oldObject = transform.GetChild(0).gameObject;

                // store the dropped GameObject temporarily
                GameObject objectDropped = eventData.pointerDrag;

                // snap the GameObject to the position of the slot
                (objectDropped.transform as RectTransform).position = (transform as RectTransform).position;
            
                // set the parent so UI masking works
                objectDropped.transform.SetParent(transform);

                // set the currentItem field to the contents of the inventory button
                // which can be used in slots that use the active item
                currentItem = eventData.pointerDrag.GetComponent<InventoryButton>().Contents;

                // move current item to other slot
                


                // snap the GameObject to the position of the slot
                (oldObject.transform as RectTransform).position = (transform as RectTransform).position;
            
                // set the parent so UI masking works
                oldObject.transform.SetParent(transform);

                // set the currentItem field to the contents of the inventory button
                // which can be used in slots that use the active item
                currentItem = eventData.pointerDrag.GetComponent<InventoryButton>().Contents;
            }



        }
    }
}
