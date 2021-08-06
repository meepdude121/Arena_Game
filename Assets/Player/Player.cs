// todo: rewrite whole thing.

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
    // make more simple! too complex
    private void Update()
    {
        // Split into GUI handle script
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
                    // use object pooling (low priority)
                    // could also split into separate weapon script
                    GameObject a = Instantiate(weapon.projectile);
                    a.transform.position = transform.position;
                    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.z = 0;
                    a.GetComponent<Bullet>().TARGET = target;
                    weapon.InternalCooldown = 0;
                }
            }
            weapon.InternalCooldown += Time.deltaTime;
            // split into separate camera script
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, -1f), ref a, 0.1f);
        }
        // split into separate camera script
        else Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, transitionPosition, ref a, 0.25f);
    }
    //TODO: Move to different scripts! too much stuff
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Entity entity = other.GetComponent<Entity>();
            Health -= entity.Damage;
            UpdateHealth();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Room"))
        {
            other.GetComponent<Room>().RoomEnter();
        }

        Entity entityScript;
        if (other.CompareTag("Enemy") && other.GetComponentInParent<Entity>().Type == EnemyType.GreenSlime)
        {
            entityScript = other.GetComponentInParent<Entity>();
            Health -= entityScript.Damage;
            entityScript.Health -= entityScript.MaxHealth;
            UpdateHealth();
        }
        else if (other.CompareTag("Enemy") && other.GetComponentInParent<Entity>().Type == EnemyType.BlueSlime)
        {
            entityScript = other.GetComponentInParent<Entity>();
            Health -= entityScript.Damage;
            UpdateHealth();
        }
        else if (other.CompareTag("Enemy") && other.GetComponentInParent<Entity>().Type == EnemyType.OrangeSlime)
        {
            entityScript = other.GetComponentInParent<Entity>();
            Health -= entityScript.Damage;
            UpdateHealth();
        }
    }
    public void UpdateHealth() => healthSliderValue = Health / maxHealth;

    // TODO: Move to different script
    public void CreateText(string Content)
	{

	}
}