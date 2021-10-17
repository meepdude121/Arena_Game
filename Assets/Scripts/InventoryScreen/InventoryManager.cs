using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	Inventory inventory;
	public static InventoryManager Instance { get; private set; }

	public static Canvas InventoryCanvas { get; private set; }

    private void Awake()
    {
		Instance = this;
		InventoryCanvas = GetComponent<Canvas>();
    }


}

struct Inventory
{
	/// <summary><para>TKey (int) = slot number</para><para>TValue (IItem) = Item</para></summary>
	Dictionary<int, IItem> MainInventory;
}
