using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public void InitItem() => StartCoroutine(Spawn());
	Renderer renderer;
	private void Awake()
	{
		renderer = GetComponent<Renderer>();
	}
	IEnumerator Spawn()
	{
		float t = 0;
		while (t <= 1f)
		{
			t += Time.deltaTime;
			renderer.material.SetFloat("Visibility", t/1f);
			yield return null;
		}
		yield return null;
	}
}
