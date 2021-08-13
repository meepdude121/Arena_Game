using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
	public float BaseDamage;
	public int BaseAmmoCount;
	public float BaseReloadTime;
	public float BaseShotsPerSecond;
	public UIManager_Main UIManager;
	
	/// <summary>
	/// Runs when player attempts to shoot.
	/// </summary>
	public abstract void OnShoot();

	/// <summary>
	/// Runs when player attempts to reload.
	/// Default behaviour: Runs Reload() coroutine.
	/// </summary>
	public virtual void OnReload() { StartCoroutine(Reload(BaseReloadTime)); }

	/// <summary>
	/// Runs when player loses energy.
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
	public void UpdateReloadProgress(float Progress) => UIManager.UpdateAmmoCount(Mathf.Clamp(Progress, 0, 1));
}
