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
                }
                else
                {
                    Debug.LogWarning("[WARN] Enemy is not assigned to a room!");
                }
                Destroy(gameObject);
            }
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