using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
	GameObject buttonPrefab;
    List<Item> Inventory = new List<Item>();
	private void Awake()
	{
		buttonPrefab = Resources.Load<GameObject>("GUIAssets/Inventory/DefaultButtonPrefab");
	}
	void OnOpenInventory()
	{
		foreach (Item item in Inventory)
		{
			//Instantiate();
		}
	}
}
