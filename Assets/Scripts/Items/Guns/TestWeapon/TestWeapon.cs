using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestWeapon : Weapon
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private ParticleSystem particles;

    // OnShoot should only be called if the weapo is able to be shot, not if the player is holding down the button
    public override void OnShoot(Vector2 targetPosition)
    {
        // Get object position on screen point
        // Ok rewrite this so it gets world space so theres less expensive calls
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        targetPosition = Camera.main.WorldToScreenPoint(targetPosition);
        targetPosition -= objectPos;

        // set up a system for pooling bullets
        GameObject newbullet = Instantiate(bullet);

        // this is terrible
        // have a function where all of this is set

        newbullet.transform.position = transform.position;
        Bullet bulletComponent = newbullet.GetComponent<Bullet>();
        bulletComponent.TargetPosition = targetPosition;
        bulletComponent.Damage = BaseDamage;
        bulletComponent.IsOwnedByPlayer = transform.parent.CompareTag("Player");
        bulletComponent.OnInstantiate();
        particles.Play();
        StartCoroutine(ShootAnimation(0.3f));
    }
}
