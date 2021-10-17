using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugItemFactory : MonoBehaviour
{

    public void Awake()
    {
        var a = GetComponent<TextMeshProUGUI>();
        if (ItemFactory.GetItemCount() > 0)
        {
            a.text = "";
            foreach (ObjectID identity in ItemFactory.getItemsByID().Keys)
            {

                a.text += $"{identity.AsString}\n";
            }
        } else
        {
            a.text = "<color=yellow>No items loaded!";
        }
    }

    public void Update()
    {
        
    }
}
