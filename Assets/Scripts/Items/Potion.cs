using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potion")]
public class Potion : ItemData
{
    public void Use(MonoBehaviour mono)
    {
        for (int i = 0; i < _actionValue.Length; i++)
        {
            var action = Instantiate(_actions[i]);
            action.Init(mono, _actionValue[i]);
        }
    }
}
