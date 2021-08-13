using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestWeapon : Weapon
{
	[SerializeField] GameObject bullet;
	private float ShootTimer = 0f;
	public override void OnShoot()
	{
		if (ShootTimer >= BaseShotsPerSecond)
		{
			Vector3 mousePos = Mouse.current.position.ReadValue();
			mousePos.z = 5.23f;
			Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
			mousePos -= objectPos;

			GameObject newbullet = Instantiate(bullet);
			newbullet.transform.position = transform.position;
			newbullet.GetComponent<Bullet>().TARGET = mousePos;

			ShootTimer = 0f;
		}
	}
	public void Update()
	{
		ShootTimer += Time.deltaTime;
		Mathf.Clamp(ShootTimer, 0, BaseShotsPerSecond);
	}
}
