using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLoot : MonoBehaviour
{
	public int EmptyDrops;
    public GameObject[] dropList;

    public void DropItem(GameObject origin)
	{
		// pick a number between -EmptyDrops and length of dropList
		int randomNumber = Random.Range(-EmptyDrops, dropList.Length);
		Debug.Log($"{{Result={randomNumber},Bounds={{{-EmptyDrops},{dropList.Length}}}");
		if (randomNumber < 0) // Landed on empty drop
			return;

		GameObject a = Instantiate(dropList[randomNumber]);
		a.transform.position = origin.transform.position;
	}
}
