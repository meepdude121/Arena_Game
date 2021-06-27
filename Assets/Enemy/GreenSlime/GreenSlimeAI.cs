// TODO:
// Comment **everything** for extra credit

using System.Collections.Generic;
using System.Collections;
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
        entityComponent.InternalBulletDelay = Random.Range(0, entityComponent.BulletDelay / 2f);
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
            if (closestEnemy != null)
            {
                if (Vector3.Distance(transform.position, closestEnemy.transform.position) < entityComponent.DistanceToStop + 2.5f)
                {
                    Vector3 direction = closestEnemy.transform.position - transform.position;
                    rb.AddForce(1.25f * entityComponent.Speed * Time.deltaTime * -direction.normalized, ForceMode.VelocityChange);
                }
            }
            if (Vector3.Distance(transform.position, Target.transform.position) > entityComponent.DistanceToStop)
            {
                Vector3 direction = Target.transform.position - transform.position;
                rb.AddForce(entityComponent.Speed * Time.deltaTime * direction.normalized, ForceMode.VelocityChange);
            }
            else if (Vector3.Distance(transform.position, Target.transform.position) < entityComponent.DistanceToStop - 0.5f)
            {
                Vector3 direction = Target.transform.position - transform.position;
                rb.AddForce(entityComponent.Speed * Time.deltaTime * -direction.normalized, ForceMode.VelocityChange);
            }

            if (entityComponent.InternalBulletDelay >= entityComponent.BulletDelay)
            {
                entityComponent.InternalBulletDelay = 0f;
                StartCoroutine(FireBullet(Target));
            }
            else
            {
                entityComponent.InternalBulletDelay += Time.deltaTime;
            }
        }
        if (entityComponent.Health <= 0)
        {
            parentRoom.EnemyCount -= 1;
            Destroy(gameObject);
        }
        // set positiveVelocity to velocity, if velocity is negative set to positive
        Vector2 positiveVelocity = new Vector2
        {
            x = rb.velocity.x < 0 ? -rb.velocity.x : rb.velocity.x, // if x = negative, invert
            y = rb.velocity.y < 0 ? -rb.velocity.y : rb.velocity.y  // if y = negative, invert
        };

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
    IEnumerator FireBullet(GameObject Target)
    {
        float t1 = 0;
        float t2 = 0;

        // t(1 or 2)f is the time for the object to finish scaling.
        // We scale the object to make it clear to the player the AI is going to shoot
        float t1f = 0.5f;
        float t2f = 0.1f;
        while (t1 < 0.5f)
        {
            t1 += Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Clamp(1f - (t1 / t1f / 2), 0.5f, 1f), Mathf.Clamp(1f - (t1 / t1f / 2), 0.5f, 1f), 1f);
            yield return null;
        }
        // force to proper scale
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        // create bullet object
        GameObject a = Instantiate(entityComponent.Projectile);
        // set bullet offset to player's velocity (where they're moving) + a random number for some spread
        Vector3 bulletOffset = new Vector3((Target.GetComponent<Rigidbody>().velocity.x / 2.5f) + Random.Range(-1f, 1f), (Target.GetComponent<Rigidbody>().velocity.y / 2.5f) + Random.Range(-1f, 1f), 0f);
        // set the bullet target position to the player position + the offset
        // angle to move in is calculated in the bullet script to save space in the AI code
        a.GetComponent<Bullet>().TARGET = Target.transform.position + bulletOffset;
        // set the bullet's position to the object firing it
        a.transform.position = transform.position;

        while (t2 < 0.25f)
        {
            t2 += Time.deltaTime;
            // set the scale to the percent of the operation complete (0f - 1f) divided by 2 to get (0f - 0.5f)
            // Mathf.Clamp limits the value to a certain range. Used here so the scale doesn't go too large or too small
            transform.localScale = new Vector3(Mathf.Clamp(0.75f + (t2 / t2f / 2), 0.5f, 1f), Mathf.Clamp(0.75f + (t2 / t2f / 2), 0.5f, 1f), 1f);
            yield return null;
        }
        // Reset scale
        transform.localScale = new Vector3(1f, 1f, 1f);
        yield return null;
    }
}