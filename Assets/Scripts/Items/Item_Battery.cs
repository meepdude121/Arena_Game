using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Battery : Item
{
	public float Capacity;
	public override void OnPickup(Collision2D collision)
	{
		collision.collider.gameObject.GetComponent<Entity>().ChangeEnergy(Capacity);
	}
}
