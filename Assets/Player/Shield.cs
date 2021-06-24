using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    float visibility = 0f;
    float TimeToTake = 0f;
    float TimeTaken = 0f;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    Renderer renderer;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void FadeOut(float _Time) => StartCoroutine(internal_FadeOut(_Time));
    public void FadeIn(float _Time) => StartCoroutine(internal_FadeIn(_Time));
    IEnumerator internal_FadeOut(float _Time)
    {
        TimeToTake = _Time;
        TimeTaken = 0f;
        while (TimeTaken <= TimeToTake)
        {
            TimeTaken += Time.deltaTime;
            // Gets 1 - progress. 1 - progress inverts the progress completion so the more it 
            // progresses, the less visible the shader is.
            visibility = 1f - TimeTaken / TimeToTake;
            // Sets the variable Visibility in the shader to the progress (0f - 1f) calculated before
            renderer.material.SetFloat("Visibility", visibility);
            // wait 1 frame
            yield return null;
        }
        // Exit the coroutine
        yield return null;
    }

    IEnumerator internal_FadeIn(float _Time)
    {
        TimeToTake = _Time;
        TimeTaken = 0f;
        while (TimeTaken <= TimeToTake)
        {
            TimeTaken += Time.deltaTime;
            // Gets the progress. TimeTaken/TimeToTake is a value between 0f-1f.
            visibility = TimeTaken / TimeToTake;
            // Sets the variable Visibility in the shader to the progress (0f - 1f) calculated before
            renderer.material.SetFloat("Visibility", visibility);
            // wait 1 frame.
            yield return null;
        }
        // Exit the coroutine
        yield return null;
    }
}