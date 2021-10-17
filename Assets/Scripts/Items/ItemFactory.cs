using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public static class ItemFactory
{
    // ItemID is Namespace:Name
    private static Dictionary<ObjectID, Type> ItemsByID;
    private static bool IsInitialized => ItemsByID != null;
    public static int GetItemCount() => getItemsByID().Count;
    public static Dictionary<ObjectID, Type> getItemsByID() 
    { 
        if (!IsInitialized)
        {
            InitializeFactory();
        }
        return ItemsByID;
    }

    public static void InitializeFactory()
    {
        if (IsInitialized) return;
        // get all classes that inherit from IItem
        var ItemTypes = from Item in Assembly.GetAssembly(typeof(IItem)).GetTypes() where typeof(IItem).IsAssignableFrom(Item) where !Item.IsAbstract where Item.IsClass select Item;

        // Find item by ID
        ItemsByID = new Dictionary<ObjectID, Type>();
        foreach (var type in ItemTypes)
        {
            var TempEffect = Activator.CreateInstance(type) as IItem;
            ItemsByID.Add(TempEffect.ItemID, type);
        }
    }

    public static IItem GetItem(ObjectID id)
    {
        InitializeFactory();

        // if item exists
        if (ItemsByID.ContainsKey(id))
        {
            // find item
            Type type = ItemsByID[id];

            // return instantiated copy of item
            var item = Activator.CreateInstance(type) as IItem;
            return item;
        }

        Debug.LogError($"Attempted to instantiate {id.AsString}, which either doesn't exist or did not load properly.\nThis could lead to instability in the AI and could crash the game.");
        return null;
    }
}