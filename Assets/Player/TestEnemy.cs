using UnityEngine;
[RequireComponent(typeof(Entity))]
public class TestEnemy : MonoBehaviour
{
	Entity entityComponent;
	GameObject[] players;
	private void Awake()
	{
		entityComponent = GetComponent<Entity>();
		players = GameObject.FindGameObjectsWithTag("Player");

	}

	private void Update()
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

		if (Vector3.Distance(transform.position, Target.transform.position) > entityComponent.DistanceToStop)
		{
			Vector3 direction = Target.transform.position - transform.position;
			transform.position += direction.normalized * entityComponent.Speed * Time.deltaTime;
		} else if (Vector3.Distance(transform.position, Target.transform.position) < entityComponent.DistanceToStop - 0.5f)
		{
			Vector3 direction = Target.transform.position - transform.position;
			transform.position -= direction.normalized * entityComponent.Speed * Time.deltaTime;
		}

		if (entityComponent.InternalBulletDelay >= entityComponent.BulletDelay)
		{
			GameObject a = Instantiate(entityComponent.Projectile);
			a.GetComponent<Bullet>().TARGET = Target.transform.position;
			a.transform.position = transform.position;
			entityComponent.InternalBulletDelay = 0f;
		}
		else
		{
			entityComponent.InternalBulletDelay += Time.deltaTime;
		}

	}
}