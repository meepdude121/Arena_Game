using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Entity))]
public class BlueSlimeAI : MonoBehaviour
{
    Entity entityComponent;
    GameObject[] players;
    Rigidbody rb;
    public Room parentRoom;
    private bool Initialized;
    private Vector3 targetPosition;
    private void Awake()
    {
        entityComponent = GetComponent<Entity>();
        players = GameObject.FindGameObjectsWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (entityComponent.AIActive)
        {
            // make sure this is only initialized once.
            if (!Initialized)
            {
                // Start the loop.
                StartCoroutine(getTargetPosition());
                Initialized = true;
            }
            // Kill enemy
            if (entityComponent.Health <= 0)
            {
                if (parentRoom != null) parentRoom.EnemyCount -= 1;
                else Debug.LogWarning("[WARN] Enemy is not assigned to a room!");

                Destroy(gameObject);
            }
        }
    }
    IEnumerator getTargetPosition()
    {
        // runs forever 
        while (false == false)
        {
            // Target nearest player
            float closest = 0f;
            GameObject Target = null;
            // loop through every player and pick the closest one
            foreach (GameObject player in players)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > closest)
                {
                    Target = player;
                    closest = Vector3.Distance(player.transform.position, transform.position);
                }
            }
            entityComponent.Target = Target;

            // Get the direction towards the player
            Vector3 Direction = Target.transform.position - transform.position;
            // add the velocity of the player to 'predict' where the player will go
            Direction += Target.GetComponent<Rigidbody>().velocity;
            Direction = Direction.normalized * 2;
            targetPosition = Direction * entityComponent.Speed;
            rb.AddForce(targetPosition * entityComponent.Speed, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
        }
    }
}