using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
	GameObject DefaultButton;
    List<Item> Inventory = new List<Item>();

	GameObject InventoryGUI;
	private void Awake()
	{
		InventoryGUI = GameObject.Find("InventoryScreen");
		DefaultButton = Resources.Load<GameObject>("GUIAssets/Inventory/ItemButtons/Fallback");
		Debug.Log(DefaultButton);

		GameObject a = Instantiate(DefaultButton);
		a.transform.SetParent(InventoryGUI.transform, false);
		//(a.transform as RectTransform).anchoredPosition = new Vector2((float)Screen.width / 2, (float)Screen.height / 2);
	}
    void UpdateInventory()
    {
		foreach (Item item in Inventory)
		{
			//Instantiate();
		}
	}

	void OnToggleInventory()
    {
		InventoryGUI.SetActive(!InventoryGUI.activeInHierarchy);
    }
}
