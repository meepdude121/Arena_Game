using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float Energy = 0;
    public float maxEnergy = 0;
    public virtual void CalculateMaxEnergy()
    {
        // when items exist do math here and run when items change (for players only)
    }
    public virtual void ChangeEnergy(float Change)
    {
        Energy = Mathf.Clamp(Energy + Change, float.NegativeInfinity, maxEnergy);
        OnEnergyChange(Change);
        if (Energy <= 0) OnEntityDeath();
    }
    public virtual void OnEntityDeath()
    {
        Destroy(gameObject);
        return;
    }
    public virtual void OnEnergyChange(float Change) {
        return;
    }
}
