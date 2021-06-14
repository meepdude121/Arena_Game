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
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

        Vector2 positiveVelocity = new Vector2();
        // set positiveVelocity to velocity, if velocity is negative set to positive
        positiveVelocity.x = rb.velocity.x < 0 ? -rb.velocity.x : rb.velocity.x;
        positiveVelocity.y = rb.velocity.y < 0 ? -rb.velocity.y : rb.velocity.y;

        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);

        if (positiveVelocity.x > positiveVelocity.y)
        {
            if (rb.velocity.x < 0)
            {
                // moving left
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
            }
            else
            {
                // moving right
                animator.SetBool("Left", false);
                animator.SetBool("Right", true);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
            }
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                // moving down
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);
            }
            else
            {
                // moving up
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Entity entity = other.GetComponent<Entity>();
            Health -= entity.Damage;
            healthSliderValue = Health / maxHealth;
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
            Debug.Log(entityScript.transform);
            Health -= entityScript.Damage;
            entityScript.Health -= entityScript.MaxHealth;
            healthSliderValue = Health / maxHealth;
        }
    }
}
