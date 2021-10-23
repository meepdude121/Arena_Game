using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TestAI : MonoBehaviour
{
    Transform player;
    Pathfinding_Unit unit;
    GameObject gun;
    GunManager gunManager;
    public LayerMask LineOfSightLayerMask;
    RaycastHit2D hit2;
    bool hasReachedEndPoint = false;

    Pathfinding_Grid grid;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        unit = GetComponent<Pathfinding_Unit>();
        gun = transform.GetChild(0).gameObject;
        gunManager = gun.GetComponent<GunManager>();
        grid = GameObject.Find("Pathfind Manager").GetComponent<Pathfinding_Grid>();
    }
    void Update()
    {
        // The result of the raycast
        RaycastHit2D hit;

        // Do a raycast from the enemy to the player. store the results in a RaycastHit2D
        hit = Physics2D.Raycast(transform.position, player.position - transform.position, 100, LineOfSightLayerMask);
        if (hit.collider != null)
        {
            // if can see the player 
            if (hit.transform == player)
            {
                // assign target position if player.position is not target position
                if (unit.target != (Vector2)player.position)
                {
                    unit.target = player.position;
                }
                AimTowards(player.position);
                gunManager.Shoot(player.position);
                hasReachedEndPoint = false;
            } 
            // if cannot see the player
            else
            {
                // and has reached end point of the player
                if (!hasReachedEndPoint) { 
                    unit.target += new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f)); 
                    hasReachedEndPoint = true;

                }
                gun.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 90));
            }
        }
        hit2 = hit;
    }
    
    void AimTowards(Vector2 target)
    {
        Vector2 objectPos = (Vector2)transform.position;
        target -= objectPos;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg));
    }
}
