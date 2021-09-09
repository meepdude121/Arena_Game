using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public abstract class Weapon : MonoBehaviour
{
    public float BaseDamage;
    public int BaseAmmoCount;
    public float BaseReloadTime;
    public float BaseShotsPerSecond;
    public UIManager_Main UIManager;
    private Color FlashColor = new Color(1, 0.64705882352f, 0, 0);
    public Light2D FlashLight;
    /// <summary>
    /// Runs when player attempts to shoot.
    /// </summary>
    public abstract void OnShoot(Vector2 Target);

    /// <summary>
    /// Runs when unit attempts to reload.
    /// Default behaviour: Runs Reload() coroutine.
    /// </summary>
    public virtual void OnReload() { StartCoroutine(Reload(BaseReloadTime)); }

    /// <summary>
    /// Runs when the unit loses energy.
    /// </summary>
    public virtual void OnTakeDamage() { }
    public IEnumerator Reload(float ActionTime)
    {
        float Progress = 0f;
        while (Progress < ActionTime)
        {
            Progress += Time.deltaTime;
            UpdateReloadProgress(Progress / ActionTime);
        }

        yield return null;
    }
    public IEnumerator ShootAnim()
    {
        float t = 0;
        float t1 = 0;
        while (t < 1)
        {
            FlashColor.a = Mathf.Clamp01(FlashColor.a = 1 - t1);

            transform.parent.localScale = new Vector3(Mathf.Lerp(0.75f, 1f, t), 1f, 1f);
            t += Time.deltaTime * 3f;
            Mathf.Clamp01(t1 += Time.deltaTime * 5f);
            t1 += Time.deltaTime * 5f;
            FlashLight.color = FlashColor;
            yield return null;
        }
        transform.parent.localScale = Vector3.one;
        FlashColor.a = 0;
        yield return null;
    }
    public void UpdateReloadProgress(float Progress) => UIManager.UpdateAmmoCount(Mathf.Clamp(Progress, 0, 1));
}
