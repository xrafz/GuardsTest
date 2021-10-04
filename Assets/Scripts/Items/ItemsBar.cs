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
        }
    }
}
