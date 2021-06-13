using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
	float visibility = 1f;
	float TimeToTake = 0f;
	float TimeTaken = 0f;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
	Renderer renderer;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
	private void Awake()
	{
		renderer = GetComponent<Renderer>();
	}

	// Requires caller component so it is possible to tell the caller's component it has finished fading in the room.
	public void FadeOut(float _Time, Room Caller) => StartCoroutine(internal_FadeOut(_Time, Caller));

	IEnumerator internal_FadeOut(float _Time, Room Caller)
	{
		TimeToTake = _Time;
		while (TimeTaken <= TimeToTake)
		{
			TimeTaken += Time.deltaTime;
			// Gets 1 - progress. 1 - progress inverts the progress completion so the more it 
			// progresses, the less visible the shader is.
			visibility = 1f - TimeTaken / TimeToTake;
			renderer.material.SetFloat("Visibility", visibility);
			yield return null;
		}
		Caller.FinishedFading();
		yield return null;
	}
}