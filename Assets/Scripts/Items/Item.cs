using Game.Item;
using UnityEngine;
public interface IItem
{
    public string Name { get; }// replace with enum for translation
    public ItemData.Rarity rarity { get; }

    // these are defined as per shot i think that would be good
    public float Damage { get; }
    public float ShootSpeed { get; }
    /// <summary>
    /// The weight of the weapon (grams)
    /// Will automatically convert to mg, kg and others
    /// </summary>
    public float Weight { get; } // unused ( for now :] ); will be taken into account with movement speed.
    public ObjectID ItemID { get; }
    public ItemType Type { get; }
    public GameObject Prefab { get; }
}
public enum ItemType
{
    NONE, WEAPON_PRIMARY, WEAPON_SECONDARY, MODE_OF_TRANSPORT, CHASSIS
}
public static class FormatUtils 
{ 
    static string FormatDamage(float Damage)
    {
        return "Error while parsing";
    }
}