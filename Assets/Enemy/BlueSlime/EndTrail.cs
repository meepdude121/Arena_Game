using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrail : MonoBehaviour
{
    public Vector3 scalePerSecond = new Vector3(1f, 1f, 1f);
    public Color colorPerSecond = new Color(255, 255, 255, 1f);
    SpriteRenderer sr;
    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (transform.localScale.x >= 0f) transform.localScale -= scalePerSecond * Time.deltaTime;
        if (sr.color.a >= 0f) sr.color -= colorPerSecond * Time.deltaTime * 2;
        if (sr.color.a <= 0f || transform.localScale == Vector3.zero)
        {
            Destroy(gameObject);
        }
    }
}
