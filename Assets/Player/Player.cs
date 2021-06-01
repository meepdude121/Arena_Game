using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    [Range(1, 1000)]
    public float Speed;
    public float Health;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector3(horizontal,0, vertical).normalized * Speed * Time.deltaTime, ForceMode.VelocityChange);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Entity entity = other.GetComponent<Entity>();
            Health -= entity.Damage;

            
        }
    }
}
