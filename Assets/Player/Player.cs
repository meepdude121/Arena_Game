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
    float healthSliderValue = 1f;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public Weapon weapon;
    public bool InTransition = false;
    public Vector3 transitionPosition;
    private float b;
    private Vector3 a;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        healthSlider.value = Mathf.SmoothDamp(healthSlider.value, healthSliderValue, ref b, 0.05f);
        healthText.text = $"{Health}/{maxHealth}";
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (!InTransition)
        {
            rb.AddForce(new Vector3(horizontal, vertical, 0).normalized * Speed * Time.deltaTime, ForceMode.VelocityChange);

            if (Input.GetKey(KeyCode.Mouse0) || Input.GetAxis("Fire1") >= 0.2f)
            {
                if (weapon.InternalCooldown >= weapon.Cooldown)
                {
                    GameObject a = Instantiate(weapon.projectile);
                    a.transform.position = transform.position;
                    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.z = 0;
                    a.GetComponent<Bullet>().TARGET = target;
                    weapon.InternalCooldown = 0;
                }
            }
            weapon.InternalCooldown += Time.deltaTime;
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, -1f), ref a, 0.1f);
        }
        else Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, transitionPosition, ref a, 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Projectile"))
        {
            Entity entity = other.GetComponent<Entity>();
            Health -= entity.Damage;
            healthSliderValue = (float)Health / maxHealth;

            if (other.CompareTag("Projectile")) Destroy(other.gameObject);
        } else if (other.CompareTag("Room"))
        {
            other.GetComponent<Room>().RoomEnter();
        }
    }
}
