using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGraphics : MonoBehaviour
{
    Transform Parent;
    GameObject MainTexParent;
    float[] Velocities = new float[2];
    void Start() {
        MainTexParent = transform.GetChild(0).gameObject;
        Parent = transform.parent;
        StartCoroutine(MoveTextures());
    }
    IEnumerator MoveTextures() {
        float y = 0f;
        while (true) {
            while (y < .09f) {
                y = Mathf.SmoothDamp(y, 0.1f, ref Velocities[0], 1f, float.PositiveInfinity);
                transform.position = new Vector3(0, y, 0) + Parent.position;
                yield return null;
            }
            while (y > -.09f) {
                y = Mathf.SmoothDamp(y, -0.1f, ref Velocities[0], 1f, float.PositiveInfinity);
                transform.position = new Vector3(0, y, 0) + Parent.position;
                yield return null;
            }           
            yield return null;
        }
    }
}
