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
        Set(item, Types.Buying);
    }

    public void Set(ItemData item, Types type)
    {
        _item = item;
        _image.sprite = _item.Sprite;

        switch (type)
        {
            case Types.Buying:
                {
                    _button.onClick.AddListener(Add);
                    break;
                }
            case Types.Selling:
                {
                    _button.onClick.AddListener(Remove);
                    break;
                }
            case Types.Using:
                {
                    _button.onClick.AddListener(Use);
                    break;
                }
        }
    }

    public void Add()
    {
        Shop.Instance.Add(_item);
    }
    public void Remove()
    {
        Shop.Instance.Remove(_item);
        Destroy(gameObject);
    }

    public void Use()
    {

    }

    public enum Types
    {
        Buying,
        Selling,
        Using
    }
}
