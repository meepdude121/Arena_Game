using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
	Object WeaponClass = null;

	private void Start()
	{
		OnChangeGun();
	}
	public void Shoot()
	{
		(WeaponClass as Weapon).OnShoot();
	}

	public void OnChangeGun()
	{
		WeaponClass = GetComponentInChildren<Weapon>();
	}
}
