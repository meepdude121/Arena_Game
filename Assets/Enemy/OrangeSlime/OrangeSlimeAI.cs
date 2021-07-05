using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSlimeAI : MonoBehaviour
{
	Entity entityComponent;
	public Room room;
	Vector2 RoomBounds;
	bool setPosition;
	Vector3 TargetPosition;
	Rigidbody rb;
	GameObject Target;
	Animator animator;
	// stores up, down, left, right
	static Vector3[] XYDirections =
	{
		Vector3.up, Vector3.down, Vector3.left, Vector3.right
	};
	// stores N, NW, W, etc.
	static Vector3[] Directions360 =
	{
		new Vector3(0, 1, 0),
		new Vector3(1, 1, 0),
		new Vector3(1, 0, 0),
		new Vector3(1, -1, 0),
		new Vector3(0, -1, 0),
		new Vector3(-1, -1, 0),
		new Vector3(-1, 0, 0),
		new Vector3(-1, 1, 0)
	};
	
	private void Awake()
	{
		entityComponent = GetComponent<Entity>();
		RoomBounds = room.Bounds;
		rb = GetComponent<Rigidbody>();
		Target = GameObject.Find("Player");
		animator = GetComponent<Animator>();
	}
	void Update()
	{
		// if velocity.magnitude is positive, animator speed is positive, otherwise it's negative
		animator.speed = rb.velocity.magnitude > 0 ? rb.velocity.magnitude / 5 : rb.velocity.magnitude / -5;
        if (entityComponent.AIActive)
		{
			if (!setPosition)
			{
				// find a random position in the room
				TargetPosition.x = Random.Range((-RoomBounds.x / 2f)+1f, (RoomBounds.x / 2f)-1f);
				TargetPosition.y = Random.Range((-RoomBounds.y / 2f)+1f, (RoomBounds.y / 2f)-1f);
				TargetPosition += room.transform.position;
				Debug.Log(TargetPosition);
				setPosition = true;
			}
			Vector3 direction = TargetPosition - transform.position;
			rb.AddForce(direction.normalized * entityComponent.Speed * Time.deltaTime, ForceMode.VelocityChange);
			if (Vector3.Distance(transform.position, TargetPosition) < 0.25f)
			{
				setPosition = false;
				int value = Random.Range(0, 1);
                switch (value)
                {
                    case 0:
                        FireProjectile(FireMode.CardinalDirections);
                        break;
                    case 1:
                        FireProjectile(FireMode.HorizVertic);
                        break;
                }
            }
			// Check whether it's time to fire a bullet
			if (entityComponent.InternalBulletDelay >= entityComponent.BulletDelay)
			{
				FireProjectile(FireMode.AtPlayer);
				entityComponent.InternalBulletDelay = 0f;
			}
			// otherwise increase the timer by the FrameTime
			else
			{
				entityComponent.InternalBulletDelay += Time.deltaTime;
			}
			if (entityComponent.Health <= 0)
			{
				if (room != null)
				{
					room.EnemyCount -= 1;
				}
				else
				{
					Debug.LogWarning("[WARN] Enemy is not assigned to a room!");
				}
				Destroy(gameObject);
			}
		}
	}
	void FireProjectile(FireMode mode)
	{
		GameObject[] bullets;
		switch (mode)
		{
			
			case FireMode.AtPlayer:
				// create an array with 1 value to store the singular bullet
				bullets = new GameObject[1];
				bullets[0] = Instantiate(entityComponent.Projectile);
				// Add bullet spread but also 'predict' where the player will go based on their velocity.
				Vector3 bulletOffset = new Vector3((Target.GetComponent<Rigidbody>().velocity.x / 2.5f) + Random.Range(-1f, 1f), (Target.GetComponent<Rigidbody>().velocity.y / 2.5f) + Random.Range(-1f, 1f), 0f);
				bullets[0].GetComponent<Bullet>().TARGET = Target.transform.position + bulletOffset;
				bullets[0].transform.position = transform.position;
				break;
			case FireMode.HorizVertic:
				bullets = new GameObject[4];
				for (int i = 0; i < 4; i++)
				{
					bullets[i] = Instantiate(entityComponent.Projectile);
					bullets[i].GetComponent<Bullet>().TARGET = transform.position + XYDirections[i];
					bullets[i].transform.position = transform.position;
				}
				break;
			case FireMode.CardinalDirections:
				StartCoroutine(Fire360Degrees());
				break;
			default:
				break;
		}
	}
	IEnumerator Fire360Degrees()
	{
		GameObject[] bullets = new GameObject[Directions360.Length];
		for (int i = 0; i < Directions360.Length; i++)
		{
			bullets[i] = Instantiate(entityComponent.Projectile);
			bullets[i].GetComponent<Bullet>().TARGET = transform.position + Directions360[i];
			bullets[i].transform.position = transform.position;
			yield return new WaitForSeconds(0.1f);
		}
	}
	enum FireMode
	{
		AtPlayer,
		HorizVertic,
		CardinalDirections
	}
}
