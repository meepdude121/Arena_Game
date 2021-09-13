using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Battery : Item
{
	public float Capacity;
	public override void OnPickup(Collider2D collision)
	{
		collision.gameObject.GetComponent<Entity>().ChangeEnergy(Capacity);
		Destroy(gameObject);
	}
}
