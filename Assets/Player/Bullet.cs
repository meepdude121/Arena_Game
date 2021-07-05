using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Vector3 TARGET;
    public Rigidbody rb;
    public float Speed;
    Vector3 direction = Vector3.zero;
    private bool setup;
    private float t;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!setup) { 
            direction = TARGET - transform.position; 
            setup = true; 
        }

        transform.position += 10 * Speed * Time.deltaTime * direction.normalized;
        t += Time.deltaTime;
        // Remove bullet after 4 seconds for performance.
        if (t > 4) Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("ColliderProvider"))
        {
            // destroy game object when hitting a collider.
            Destroy(gameObject);
        }
    }
}