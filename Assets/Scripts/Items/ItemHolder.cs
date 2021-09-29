using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private ItemData _item;
    public ItemData Item => _item;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Button _button;

    public void Set(ItemData item)
    {
        Set(item, false);
    }

    public void Set(ItemData item, bool buying) //buying/selling bool
    {
        _item = item;
        _image.sprite = _item.Sprite;

        //_button.onClick.
        //дописать разный функционал кнопок в зависимости от bool
    }

    public void Add()
    {
        Shop.Instance.Add(_item);
    }
    public void Remove()
    {
        Shop.Instance.Remove(_item);
    }
}
