using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private ItemData _item;
    public ItemData Item => _item;

    private Shop _shop;

    private void Start()
    {
        _shop = Shop.Instance;
    }

    public void Add()
    {
        _shop.Add(_item);
    }
    public void Remove()
    {
        _shop.Remove(_item);
    }
}
