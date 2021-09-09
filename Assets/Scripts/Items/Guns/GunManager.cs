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
    public void Shoot(Vector2 targetPosition)
    {
        (WeaponClass as Weapon).OnShoot(targetPosition);
    }

    public void OnChangeGun()
    {
        WeaponClass = GetComponentInChildren<Weapon>();
    }
}
