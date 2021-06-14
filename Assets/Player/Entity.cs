using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for storing information about the enemy. Different enemies also have subclasses that actually contain logic.
/// </summary>
public class Entity : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public float Damage;
    [HideInInspector]
    public float DistanceToTarget;
    [HideInInspector]
    public GameObject Target;
    public float BulletDelay;
    [HideInInspector]
    public float InternalBulletDelay;
    public float DistanceToStop;
    public float Speed;

    public GameObject Projectile;
    public bool AIActive;
    public EnemyType Type;
}
public enum EnemyType
{
    GreenSlime,
    BlueSlime,
    Miniboss_SlimePrince
}