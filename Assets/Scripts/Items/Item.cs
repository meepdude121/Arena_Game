using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public Layer AcceptableLayer;
	
    public abstract void OnPickup(Collider2D collision);
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.layer == (int)AcceptableLayer)
			OnPickup(collision); 
	}

	public enum Layer
	{
		Default,
		TransparentFX,
		IgnoreRaycast,
		Water,
		UI,
		PostProcessing,
		NotInReflection,
		Empty0,
		Empty1,
		Empty2,
		Enemy,
		Player,
		NotInPathfind
	}
}
