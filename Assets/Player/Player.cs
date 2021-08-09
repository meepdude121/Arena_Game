// todo: rewrite whole thing.

using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
	PlayerInput Player_Input;

	private Rigidbody Player_Rigidbody;

	// later on:
	// change to Player_BaseSpeed and have Player_SpeedModifiers
	// maybe use a void that adds to a list of speed modifiers like
	// Player_EditSpeedModifier(id:"Locomotion_Modifier", amount:1.2f, type:SpeedModifier.ADD);
	// adds to a dictionary with <string, SpeedModifier> where SpeedModifier is a struct containing data for amount and type
	// when it changes recalculate player speed and set Player_Speed
	private float Player_Speed = 1.5f;
	// later on:
	// change to Player_BaseEnergyCapacity and have Player_EnergyCapacityModifiers
	private float Player_Energy = 100f;
	private float Player_EnergyCapacity = 100f;

	[SerializeField] private UIManager_Main UIManager;

	public bool Player_CanMove = true;
    public IWeapon Weapon;
	#region Input Variables

	private Vector2 Input_Move = Vector2.zero;
	private bool Input_Fire = false;
	private Vector2 Input_TurnGun = Vector2.zero;

	#endregion

	private void Awake()
	{
        Weapon.AttemptFire();
		Player_Input = new PlayerInput();

		Player_Input.Player.Move.started += context => Input_Move = context.ReadValue<Vector2>();
		Player_Input.Player.Move.performed += context => Input_Move = context.ReadValue<Vector2>();
        Player_Input.Player.Move.canceled += context => Input_Move = context.ReadValue<Vector2>();

        Player_Input.Player.Fire.performed += context => Input_Fire = context.ReadValueAsButton();

        Player_Input.Player.TurnGun.started += context => Input_TurnGun = context.ReadValue<Vector2>();
        Player_Input.Player.TurnGun.performed += context => Input_TurnGun = context.ReadValue<Vector2>();
        Player_Input.Player.TurnGun.canceled += context => Input_TurnGun = context.ReadValue<Vector2>();
    }
	private void Start()
	{
		Player_Rigidbody = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		if (Player_CanMove)
		{
			Player_Rigidbody.AddForce(new Vector3(Input_Move.x, Input_Move.y, 0f) * Player_Speed * 100 * Time.deltaTime, ForceMode.VelocityChange);

            if (Input_Fire)
			{

			}
		}
	}
	#region Internal function garbage
	private void OnEnable()
	{
		Player_Input.Enable();
	}
	private void OnDisable()
	{
		Player_Input.Disable();
	}
	#endregion
	/*
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

        // Convert to new input system please god damn this system sucks ass huh
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
    // this is actually completely useless because there will be a different system for picking up items and such
    public void CreateText(string Content)
	{

	}
    */
}