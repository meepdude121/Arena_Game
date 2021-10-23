using System.Collections;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{

    public float BaseDamage;

    private float TimeSinceLastShot;
    /// <summary>
    /// Weapon speed (shots per second).
    /// </summary>
    public float WeaponSpeed;


    public void OnAttemptShoot(Vector2 Target) {
        if (TimeSinceLastShot >= WeaponSpeed) {

            TimeSinceLastShot = 0f;
            OnShoot(Target);
        }
    }

    /// <summary>
    /// Runs when unit shoots.
    /// </summary>
    public abstract void OnShoot(Vector2 Target);

    /// <summary>
    /// Runs when the unit loses energy.
    /// </summary>
    public virtual void OnTakeDamage() { }

    public IEnumerator ShootAnimation(float TimeToTake) {
        float timePassed = 0;
        while (timePassed < TimeToTake) {
            // Smoothly transition from 0.75f to 1f based on TimePassed / TimeToTake
            transform.parent.localScale = new Vector3(
                Mathf.Lerp(0.75f, 1f, timePassed / TimeToTake), 
                1f, 
                1f);
                timePassed += Time.deltaTime * 3;

                // Wait for next frame
                yield return null;
        }
        // make sure scale is exactly (1,1,1)
        transform.parent.localScale = Vector3.one;
    }

    private void Update() => TimeSinceLastShot += Time.deltaTime;
}
