using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryButton : MonoBehaviour
{

	public PlayerItem item;

	Button Object_Button;
	TextMeshProUGUI Object_Title;
	
	public void UpdateButton()
	{
		Object_Button.image.color = Game.Item.ItemData.GetRarityColor(item.rarity);
	}
}
