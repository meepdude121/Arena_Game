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
	[SerializeField] private float Player_Energy = 100f;
	private float Player_EnergyCapacity = 100f;

	internal float Player_Energy_LastFrame = -1f;

	private Vector2 Input_Move = Vector2.zero;
	private bool Input_Fire = false;

	[SerializeField] private UIManager_Main UIManager;
	[SerializeField] private GunManager gunManager;

	private void Awake()
	{
		Player_Input = new PlayerInput();

		Player_Input.Player.Move.started += context => Input_Move = context.ReadValue<Vector2>();
		Player_Input.Player.Move.performed += context => Input_Move = context.ReadValue<Vector2>();
		Player_Input.Player.Move.canceled += context => Input_Move = context.ReadValue<Vector2>();

		Player_Input.Player.Fire.started += context => Input_Fire = context.ReadValueAsButton();
		Player_Input.Player.Fire.performed += context => Input_Fire = context.ReadValueAsButton();
		Player_Input.Player.Fire.canceled += context => Input_Fire = context.ReadValueAsButton();

	}
	private void Start()
	{
		Player_Rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (UIManager.AcceptInput) Player_Rigidbody.AddForce(new Vector3(Input_Move.x, Input_Move.y, 0f) * Player_Speed * 100 * Time.deltaTime, ForceMode.VelocityChange);
		if (UIManager.AcceptInput && Input_Fire) gunManager.Shoot();

		if (Player_Energy > Player_EnergyCapacity) Player_Energy = Player_EnergyCapacity;
		if (Player_Energy_LastFrame != Player_Energy) UIManager.OnEnergyChange(Player_Energy, Player_EnergyCapacity);

		Player_Energy_LastFrame = Player_Energy;
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
}