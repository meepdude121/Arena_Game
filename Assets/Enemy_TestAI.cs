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
    Entity playerEntity;

    Pathfinding_Grid grid;

    private void Awake() 
    {
        player = GameObject.Find("Player").transform;
        playerEntity = player.GetComponent<Entity>();
        unit = GetComponent<Pathfinding_Unit>();
        gun = transform.GetChild(0).gameObject;
        gunManager = gun.GetComponent<GunManager>();
        grid = GameObject.Find("Pathfind Manager").GetComponent<Pathfinding_Grid>();
    }
    void Update()
    {
        // Don't check this! because the map doesnt have walls there will never be an instance where
        // the player is not in line of sight
        #region Unused line of sight code
        /*
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
        */
        #endregion
        if (playerEntity.Energy > 0) {
            // assign target position if player.position is not target position
            if (unit.target != (Vector2)player.position)
                unit.target = player.position;

            AimTowards(player.position);
            gunManager.Shoot(player.position);
            hasReachedEndPoint = false;
        } else {
            unit.target = Vector2.zero;
            gun.transform.rotation = Quaternion.Euler(0, 0, gun.transform.rotation.eulerAngles.z + (90 * Time.deltaTime));
            // this is just where they go for some reason
            if (transform.position == new Vector3(-0.25f, -0.25f, 0)) gameObject.SetActive(false);
        }

    }
    
    void AimTowards(Vector2 target)
    {
        Vector2 objectPos = (Vector2)transform.position;
        target -= objectPos;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg));
    }
}
