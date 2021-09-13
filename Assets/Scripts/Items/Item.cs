using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public Layer AcceptableLayer;
	
    public abstract void OnPickup(Collision2D collision);
	private void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log("OnCollisionEnter2D called on item");
		if (collision.collider.gameObject.CompareLayer((int)AcceptableLayer))
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
		Enemy,
		Player,
		NotInPathfind
	}
}
