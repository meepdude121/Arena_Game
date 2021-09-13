using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_DefaultBehaviour : Entity
{
	public override void OnEntityDeath()
	{
		GetComponent<EntityLoot>().DropItem(gameObject);
		base.OnEntityDeath();
	}

}
