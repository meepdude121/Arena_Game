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
        // check whether line of sight (los) includes player
        RaycastHit2D hit;

        // Do a raycast from the enemy to the player, including 
        hit = Physics2D.Raycast(transform.position, player.position - transform.position, 100, LineOfSightLayerMask);
        if (hit.collider != null)
        {
            // if can see the player 
            if (hit.transform == player)
            {
                // assign target position if player.position != target position
                if (unit.target != (Vector2)player.position)
                {
                    unit.target = player.position;
                }
                AimTowards(player.position);
                gunManager.Shoot(player.position);
            } 
            // if cannot see the player
            else
            {
                // and has reached last seen point of the player
                if (grid.NodeFromWorldPoint(transform.position) == grid.NodeFromWorldPoint(unit.target))
                {
                    gun.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 60));
                }
            }
        }
        hit2 = hit;
    }
    private void OnDrawGizmos()
    {
        if (hit2.collider != null)
        {
            
            Gizmos.DrawLine(transform.position, hit2.point);
            Gizmos.color = new Color(1, 1, 1, 0.252f);
            Gizmos.DrawLine(transform.position, player.position);

        }

    }
    
    void AimTowards(Vector2 target)
    {
        Vector2 objectPos = (Vector2)transform.position;
        target -= objectPos;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg));
    }
}
