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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 10: // collision is with enemy
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                if (origin.CompareTag("Player"))
				{
                    collision.collider.transform.GetComponent<Entity>().ChangeEnergy(-damage);
                    Destroy(gameObject); 
                }
                break;

            case 11: // collision is with player
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                if (origin.CompareTag("Enemy"))
                {
                    collision.collider.transform.GetComponent<Entity>().ChangeEnergy(-damage);
                    Destroy(gameObject);
                }
                break;

            case 12: // Collision is with environment
                Destroy(gameObject);
                break;

            default:
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                return;
        }
    }
}