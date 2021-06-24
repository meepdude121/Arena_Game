using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Entity))]
public class GreenSlimeAI : MonoBehaviour
{
    Entity entityComponent;
    GameObject[] players;
    Rigidbody rb;
    Animator animator;
    public Room parentRoom;
    private void Awake()
    {
        entityComponent = GetComponent<Entity>();
        players = GameObject.FindGameObjectsWithTag("Player");
        entityComponent.InternalBulletDelay = Random.Range(0, entityComponent.BulletDelay);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
            Vector3 velocity = Vector3.zero;
            if (closestEnemy != null)
            {
                if (Vector3.Distance(transform.position, closestEnemy.transform.position) < entityComponent.DistanceToStop + 2.5f)
                {
                    Vector3 direction = closestEnemy.transform.position - transform.position;
                    rb.AddForce(-direction.normalized * entityComponent.Speed * 1.25f * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            if (Vector3.Distance(transform.position, Target.transform.position) > entityComponent.DistanceToStop)
            {
                Vector3 direction = Target.transform.position - transform.position;
                rb.AddForce(direction.normalized * entityComponent.Speed * Time.deltaTime, ForceMode.VelocityChange);
            }
            else if (Vector3.Distance(transform.position, Target.transform.position) < entityComponent.DistanceToStop - 0.5f)
            {
                Vector3 direction = Target.transform.position - transform.position;
                rb.AddForce(-direction.normalized * entityComponent.Speed * Time.deltaTime, ForceMode.VelocityChange);
            }

            if (entityComponent.InternalBulletDelay >= entityComponent.BulletDelay)
            {
                GameObject a = Instantiate(entityComponent.Projectile);
                Vector3 bulletOffset = new Vector3((Target.GetComponent<Rigidbody>().velocity.x / 2.5f) + Random.Range(-1f, 1f), (Target.GetComponent<Rigidbody>().velocity.y / 2.5f) + Random.Range(-1f, 1f), 0f);
                a.GetComponent<Bullet>().TARGET = Target.transform.position + bulletOffset;
                a.transform.position = transform.position;
                entityComponent.InternalBulletDelay = 0f;
            }
            else
            {
                entityComponent.InternalBulletDelay += Time.deltaTime;
            }
            rb.AddForce(velocity, ForceMode.VelocityChange);
        }
        if (entityComponent.Health <= 0)
        {
            parentRoom.EnemyCount -= 1;
            Destroy(gameObject);
        }

        Vector2 positiveVelocity = new Vector2();
        // set positiveVelocity to velocity, if velocity is negative set to positive
        positiveVelocity.x = rb.velocity.x < 0 ? -rb.velocity.x : rb.velocity.x;
        positiveVelocity.y = rb.velocity.y < 0 ? -rb.velocity.y : rb.velocity.y;

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
}