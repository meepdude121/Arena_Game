using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour
{
	InventoryButton_Data data;

	Button Object_Button;
	TextMeshProUGUI Object_Title;
	public GameObject hoverParent;
	public void OnCreateButton(InventoryButton_Data data) // required to set up new button. Call when instantiating a new prefab
	{
		this.data = data;
		Object_Button.image.color = data.color;
        
		
	}

    public void OnPointerEnter()
    {
		hoverParent.SetActive(true);
    }
	public void OnPointerExit()
	{
		hoverParent.SetActive(false);
	}
}

public struct InventoryButton_Data
{
	public PlayerItem item;
	public Color color => Game.Item.ItemData.GetRarityColor(item.rarity);
}
