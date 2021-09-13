using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestWeapon : Weapon
{
    [SerializeField] GameObject bullet;
    private float ShootTimer = 0f;
    private ParticleSystem particles;
    public override void OnShoot(Vector2 targetPosition)
    {
        if (ShootTimer >= BaseShotsPerSecond)
        {
            Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            targetPosition = Camera.main.WorldToScreenPoint(targetPosition);
            targetPosition -= objectPos;

            GameObject newbullet = Instantiate(bullet);
            newbullet.transform.position = transform.position;
            Bullet bulletComponent = newbullet.GetComponent<Bullet>();
            bulletComponent.TARGET = targetPosition;
            bulletComponent.damage = 5;
            bulletComponent.origin = gameObject.transform.parent.parent.gameObject;

            ShootTimer = 0f;
            particles.Play();
            StartCoroutine(ShootAnim());
        }
    }
    public void Update()
    {
        ShootTimer += Time.deltaTime;
        Mathf.Clamp(ShootTimer, 0, BaseShotsPerSecond);
    }
    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }
}
