using Game.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon_Item : IItem
{
    public string Name => "TestWeapon";
    public ItemData.Rarity rarity => ItemData.Rarity.common;
    public float Damage => 3f;
    public float ShootSpeed => 1f;
    public float Weight => 502;
    public ObjectID ItemID => new ObjectID().StringToItemID("game:testweapon");
    public ItemType Type => ItemType.WEAPON_PRIMARY;
    public GameObject Prefab => throw new System.NotImplementedException();
}
