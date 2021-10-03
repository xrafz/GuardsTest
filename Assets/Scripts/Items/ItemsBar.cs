using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBar : MonoBehaviour
{
    [SerializeField]
    private ItemHolder _prefab;
    [SerializeField]
    private Transform _container;

    public List<ItemData> Items { get; private set; }

    public static ItemsBar Instance;

    private void Awake()
    {
        Instance = this;

        Items = GameSession.Items;

        foreach(ItemData item in Items)
        {
            var newItem = Instantiate(_prefab, _container);
            newItem.Set(item, ItemHolder.Types.Using);
        }
    }
}
