using UnityEngine;
/// <summary>
/// todo: clean up this code.<para/>also completely redo this its so messy oh god why did i do this
/// </summary>
public class Bullet : MonoBehaviour
{
    public GameObject origin;
    public Vector3 TARGET;
    public float Speed;
    Vector3 direction = Vector3.zero;
    private bool setup;
    private float t;
    public float damage;
    private void Update()
    {
        if (!setup)
        {
            direction = TARGET - transform.position;
            setup = true;
        }

        transform.position += 10 * Speed * Time.deltaTime * direction.normalized;
        t += Time.deltaTime;
        // Remove bullet after 4 seconds for performance.
        if (t > 4) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
		{
            switch (collision.gameObject.layer)
            {
                case 10: // collision is with enemy
                    if (origin.CompareTag("Player"))
                    {
                        collision.transform.GetComponent<Entity>().ChangeEnergy(-damage);
                        Destroy(gameObject);
                    }
                    break;

                case 11: // collision is with player
                    if (origin.CompareTag("Enemy"))
                    {
                        collision.transform.GetComponent<Entity>().ChangeEnergy(-damage);
                        Destroy(gameObject);
                    }
                    break;

                case 12: // Collision is with environment
                    Destroy(gameObject);
                    break;

                default:
                    return;
            }
        }
    }
}