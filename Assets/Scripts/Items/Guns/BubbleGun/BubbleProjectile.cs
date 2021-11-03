using System.Collections;
using UnityEngine;
/// <summary> redo all of this in a system similar to IItem. </summary>

public class BubbleProjectile : MonoBehaviour
{
    public bool IsOwnedByPlayer;
    public Vector3 TargetPosition;

    public float Speed;
    public float Damage;

    private Vector3 Direction;
    private Rigidbody2D rb2D;
    
    public void OnInstantiate() {

        // set direction
        // Normalize to have a magnitude within bounds of -1 and 1.
        // This removes an issue where if the mouse is in a certain position the bullet will move faster.
        Direction = Vector3.Normalize(TargetPosition - transform.position);
        StartCoroutine(afterXTime(0.3f));
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(Direction * Speed * 3f, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
		{
            switch (collision.gameObject.layer)
            {
                // collision is with enemy
                case 10: 
                    if (IsOwnedByPlayer && collision.gameObject)
                    {
                        collision.transform.GetComponent<Entity>().ChangeEnergy(-Damage);
                        if (gameObject) Destroy(gameObject);
                    }
                    break;

                // collision is with player
                case 11: 
                    if (!IsOwnedByPlayer && collision.gameObject)
                    {
                        collision.transform.GetComponent<Entity>().ChangeEnergy(-Damage);
                        if (gameObject) Destroy(gameObject);
                    }
                    break;
                // Collision is with environment
                case 12: 
                    if (gameObject) Destroy(gameObject);
                    break;

                // collision is with an unknown object
                default:
                    return;
            }
        }
    }

    IEnumerator afterXTime(float Time) {
        yield return new WaitForSeconds(Time);
        Destroy(gameObject);
    }
}