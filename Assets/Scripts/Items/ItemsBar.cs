using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBar : MonoBehaviour
{
    [SerializeField]
    private ItemHolder _prefab;
    [SerializeField]
    private Transform _container;

    private List<Type> _usableTypes = new List<Type>
    {
        typeof(Potion)
    };

    private List<Type> _passiveItems = new List<Type>
    {
        typeof(Rune),
    };

    public static ItemsBar Instance;

    private void Awake()
    {
        Instance = this;

        var items = GameSession.Items;

        foreach(ItemData item in items)
        {
            if (_usableTypes.Contains(item.GetType()))
            {
                var newItem = Instantiate(_prefab, _container);
                newItem.Set(item, ItemHolder.Types.SelectAndUse);
            }
            else if (_passiveItems.Contains(item.GetType()))
            {
                var newItem = Instantiate(item);
                newItem.Use(this);
            }
        }
    }
}
