using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Entity))]
public class TestEnemy : MonoBehaviour
{
    Entity entityComponent;
    GameObject[] players;
    private void Awake()
    {
        entityComponent = GetComponent<Entity>();
        players = GameObject.FindGameObjectsWithTag("Player");
        entityComponent.InternalBulletDelay = Random.Range(0, entityComponent.BulletDelay);
    }

    private void Update()
    {
        GameObject[] _Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> Enemies = new List<GameObject>();
        foreach (GameObject e in _Enemies)
        {
            if (e != gameObject)
            {
                Enemies.Add(e);
            }
        }
        if (entityComponent.AIActive)
        {
            float closest = 0f;
            GameObject Target = null;
            foreach (GameObject player in players)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > closest)
                {
                    Target = player;
                    closest = Vector3.Distance(player.transform.position, transform.position);
                }
            }
            entityComponent.Target = Target;

            float closestEnemyDistance = float.PositiveInfinity;
            GameObject closestEnemy = null;
            foreach (GameObject Enemy in Enemies)
            {
                if (Vector3.Distance(Enemy.transform.position, transform.position) < closestEnemyDistance)
                {
                    closestEnemy = Enemy;
                    closestEnemyDistance = Vector3.Distance(Enemy.transform.position, transform.position);
                }
            }

            // if (distance to closestEnemy) < distanceToStop + 2
            if (closestEnemy != null)
            {
                if (Vector3.Distance(transform.position, closestEnemy.transform.position) < entityComponent.DistanceToStop - 1.25f)
                {
                    Vector3 direction = closestEnemy.transform.position - transform.position;
                    transform.position -= direction.normalized * entityComponent.Speed * Time.deltaTime;
                }
            }
            if (Vector3.Distance(transform.position, Target.transform.position) > entityComponent.DistanceToStop)
            {
                Vector3 direction = Target.transform.position - transform.position;
                transform.position += direction.normalized * entityComponent.Speed * Time.deltaTime;
            }
            else if (Vector3.Distance(transform.position, Target.transform.position) < entityComponent.DistanceToStop - 0.5f)
            {
                Vector3 direction = Target.transform.position - transform.position;
                transform.position -= direction.normalized * entityComponent.Speed * Time.deltaTime;
            }

            if (entityComponent.InternalBulletDelay >= entityComponent.BulletDelay)
            {
                GameObject a = Instantiate(entityComponent.Projectile);
                a.GetComponent<Bullet>().TARGET = Target.transform.position;
                a.transform.position = transform.position;
                entityComponent.InternalBulletDelay = 0f;
            }
            else
            {
                entityComponent.InternalBulletDelay += Time.deltaTime;
            }
        }
        if (entityComponent.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}