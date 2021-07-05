using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrail : MonoBehaviour
{
    public int ClonesPerSecond = 10;
    private SpriteRenderer sr;
    private Rigidbody rb;
    private Transform tf;
    public Vector3 scalePerSecond = new Vector3(1f, 1f, 1f);
    public Color colorPerSecond = new Color(255, 255, 255, 1f);
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(trail());
    }

    IEnumerator trail()
    {
        while (false == false)
        {
            if (rb.velocity.sqrMagnitude > 4f)
            {
                var clone = new GameObject("trailClone");
                clone.transform.position = tf.position;
                clone.transform.localScale = tf.localScale;
                var cloneRend = clone.AddComponent<SpriteRenderer>();
                cloneRend.sprite = sr.sprite;
                cloneRend.sortingOrder = sr.sortingOrder - 1;
                EndTrail a = clone.gameObject.AddComponent<EndTrail>();
                a.scalePerSecond = scalePerSecond;
                a.colorPerSecond = colorPerSecond;
            }
            yield return new WaitForSeconds(1f / ClonesPerSecond);
        }
    }
}

