using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region interface
    // I used this to activate an IEnumerator instead of just activating it because it makes it easier to edit the function. 
    // IEnumerators also run over time, making it good for animating things.
    public void Anim_OpenDoor()
    {
        StartCoroutine(OpenDoor());
    }
    public void Anim_CloseDoor()
    {
        StartCoroutine(CloseDoor());
    }
    #endregion
    IEnumerator OpenDoor()
    {
        float TimeTaken = 0f;
        const float TimeToTake = 0.5f;
        while (TimeTaken <= TimeToTake)
        {
            TimeTaken += Time.deltaTime;
            // Gets 1 - progress. 1 - progress inverts the progress completion so the more it 
            // progresses, the less visible the shader is.
            float visibility = 1f - TimeTaken / TimeToTake;
            gameObject.GetComponent<Renderer>().material.SetFloat("Visibility", visibility);
            yield return null;
        }
        yield return null;
    }
    IEnumerator CloseDoor()
    {
        float TimeTaken = 0f;
        const float TimeToTake = 0.5f;
        while (TimeTaken <= TimeToTake)
        {
            TimeTaken += Time.deltaTime;
            // Gets the progress.
            float visibility = TimeTaken / TimeToTake;
            // Set the visibility of the shader.
            gameObject.GetComponent<Renderer>().material.SetFloat("Visibility", visibility);
            yield return null;
        }
        yield return null;
    }
}
