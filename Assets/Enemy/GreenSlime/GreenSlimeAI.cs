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
        // get every enemy
        GameObject[] _Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> Enemies = new List<GameObject>();
        // loop through every enemy
        foreach (GameObject e in _Enemies)
        {
            // if the current looping enemy is NOT this enemy
            // this allows the use of the list without having
            // to check whether the enemy is the current enemy
            if (e != gameObject)
            {
                Enemies.Add(e);
            }
        }
        if (entityComponent.AIActive)
        {
            // Find closest player in list of players. Currently only returns the player because 
            // there is no multiplayer support.
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
            // set the target in the target value in the Entity component.
            entityComponent.Target = Target;

            // If there are no other enemies the closest enemy is an infinite distance away.
            float closestEnemyDistance = float.PositiveInfinity;
            GameObject closestEnemy = null;
            // loop through each enemy
            foreach (GameObject Enemy in Enemies)
            {
                if (Vector3.Distance(Enemy.transform.position, transform.position) < closestEnemyDistance)
                {
                    closestEnemy = Enemy;
                    closestEnemyDistance = Vector3.Distance(Enemy.transform.position, transform.position);
                }
            }
            // if there is a closest enemy
            if (closestEnemy != null)
            {
                // if enemy is too close to a different enemy, move away from the enemy
                if (Vector3.Distance(transform.position, closestEnemy.transform.position) < entityComponent.DistanceToStop + 2.5f)
                {
                    Vector3 direction = closestEnemy.transform.position - transform.position;
                    rb.AddForce(1.25f * entityComponent.Speed * Time.deltaTime * -direction.normalized, ForceMode.VelocityChange);
                }
            }
            float DistanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
            // if enemy is too far away from the player, move towards it
            if (DistanceToTarget > entityComponent.DistanceToStop)
            {
                // calculate direction by taking away target position by this decision and
                // normalizing it. Normalizing makes the vector3 have a magnitude of 1.
                Vector3 direction = Target.transform.position - transform.position;
                rb.AddForce(entityComponent.Speed * Time.deltaTime * direction.normalized, ForceMode.VelocityChange);
            }
            // if enemy is too close to the player, move away
            else if (DistanceToTarget < entityComponent.DistanceToStop - 0.5f)
            {
                // calculate direction by taking away target position by this decision and
                // normalizing it. Normalizing makes the vector3 have a magnitude of 1.
                Vector3 direction = Target.transform.position - transform.position;
                // -direction.normalized reverses the direction to move in.
                rb.AddForce(entityComponent.Speed * Time.deltaTime * -direction.normalized, ForceMode.VelocityChange);
            }
            // if the internal bullet delay is greater than or equal to the standard bullet delay
            if (entityComponent.InternalBulletDelay >= entityComponent.BulletDelay)
            {
                // Reset bullet delay
                entityComponent.InternalBulletDelay = 0f;
                // fire a bullet.
                StartCoroutine(FireBullet(Target));
            }
            // Increment the internal bullet delay by the frame time.
            entityComponent.InternalBulletDelay += Time.deltaTime;
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