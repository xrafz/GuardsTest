using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public Dictionary<Item, int> Items { get; private set; }

}
