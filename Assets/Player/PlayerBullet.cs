using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// See Bullet.cs for documentation: this just replaces the tag search with the Enemy tag
public class PlayerBullet : MonoBehaviour
{
    Entity bulletEntity;
    void Awake()
    {
        bulletEntity = GetComponent<Entity>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Entity entity = collision.gameObject.GetComponent<Entity>();
            entity.Health -= bulletEntity.Damage;
            Destroy(gameObject);
        }
    }
}
