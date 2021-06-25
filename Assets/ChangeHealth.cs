using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealth : MonoBehaviour
{
	public int ChangeValue;
	public bool MaxHealth;
	public bool Consumed;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Player player = other.gameObject.GetComponent<Player>();
			if (MaxHealth && !Consumed)
			{
				Debug.Log("a");
				Consumed = true;
				player.maxHealth += ChangeValue;
				player.Health += ChangeValue;
				player.UpdateHealth();
				if (ChangeValue > 0) player.CreateText($"Max Health +{ChangeValue}");
				else player.CreateText($"Max Health -{ChangeValue}");
				Destroy(gameObject);
			} else if (!MaxHealth && !Consumed && player.Health < player.maxHealth)
			{
				Debug.Log("b");
				Consumed = true;
				player.Health += ChangeValue;
				// Force the value between 0 - MaxHealth
				Mathf.Clamp(player.Health, 0, player.maxHealth);
				player.UpdateHealth();
				if (ChangeValue > 0) player.CreateText($"Health +{ChangeValue}");
				else player.CreateText($"Health -{ChangeValue}");
				Destroy(gameObject);
			}

		}
	}
}
