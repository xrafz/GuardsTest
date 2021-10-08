using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenItems : MonoBehaviour
{
    [SerializeField]
    private ItemHolder _prefab;
    [SerializeField]
    private Transform _container;

    private void Awake()
    {
        var items = GameSession.Items;

        foreach (ItemData item in items)
        {
            if (item.GetType() == typeof(Treasure))
            {
                var newItem = Instantiate(_prefab, _container);
                newItem.Set(item, ItemHolder.Types.SimpleUse);
            }
        }
    }
}
