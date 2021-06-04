using UnityEngine;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    [Range(1, 1000)]
    public float Speed;
    public float Health;
    public float maxHealth;

    public Slider healthSlider;

    public Weapon weapon;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector3(horizontal, vertical, 0).normalized * Speed * Time.deltaTime, ForceMode.VelocityChange);

        if (Input.GetKey(KeyCode.Mouse0))
		{
            if ()
		}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Projectile"))
        {
            Entity entity = other.GetComponent<Entity>();
            Health -= entity.Damage;
            healthSlider.value = Health / maxHealth;

            if (other.CompareTag("Projectile")) Destroy(other.gameObject);
        }
    }
}
