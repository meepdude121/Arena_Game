using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Entity))]
public class BlueSlimeAI : MonoBehaviour
{
    Entity entityComponent;
    GameObject[] players;
    Rigidbody rb;
    //Animator animator;
    public Room parentRoom;
    private bool Initialized;
    private Vector3 targetPosition;
    private void Awake()
    {
        entityComponent = GetComponent<Entity>();
        players = GameObject.FindGameObjectsWithTag("Player");
        entityComponent.InternalBulletDelay = Random.Range(0, entityComponent.BulletDelay);
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
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
            if (!Initialized)
			{
                StartCoroutine(getTargetPosition());
                Initialized = true;
			}
            if (entityComponent.Health <= 0)
            {
                if (parentRoom != null)
				{
                    parentRoom.EnemyCount -= 1;
                } else
				{
                    Debug.LogWarning("[WARN] Enemy is not assigned to a room!");
				}
                Destroy(gameObject);
            }

            // Finds the correct way to be facing.
            /*#region Animation
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
            #endregion*/
        }
    }
	IEnumerator getTargetPosition()
	{
        // runs forever 
        while (false == false)
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
            
            Vector3 Direction = Target.transform.position - transform.position;
            Direction += Target.GetComponent<Rigidbody>().velocity;
            Direction.Normalize();
            Direction *= 2;
            targetPosition = Direction * entityComponent.Speed;
            rb.AddForce(targetPosition * entityComponent.Speed, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
		}
	}
}