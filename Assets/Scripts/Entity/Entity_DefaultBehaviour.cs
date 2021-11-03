using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_DefaultBehaviour : Entity
{
	GameObject TextGameObject;
	private void Awake() {
		if (transform.CompareTag("Player")) {
			TextGameObject = GameObject.Find("GameOverText");
			TextGameObject.SetActive(false);
		}
	}
	public override void OnEntityDeath()
	{
		if (transform.CompareTag("Enemy")) {

			GetComponent<EntityLoot>().DropItem(gameObject);
			EnemyManager.instance.OnEnemyDeath(this);
			base.OnEntityDeath();
		} 
		else if (transform.CompareTag("Player")) {

			TextGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"<color=#f33>Game Over!</color>\nScore: {Mathf.RoundToInt(EnemyManager.instance.LocalDifficulty * 1000)}";
			TextGameObject.SetActive(true);
			
			Player.instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Player.instance.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
}
