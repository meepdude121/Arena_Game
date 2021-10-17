using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
	public int AcceptableLayer;
    public abstract void OnPickup(Collider2D collision);
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.layer == AcceptableLayer)
			OnPickup(collision); 
	}
}
