using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public Dictionary<ItemData, int> Items { get; private set; } = new Dictionary<ItemData, int>(); //   item | quantity

}
