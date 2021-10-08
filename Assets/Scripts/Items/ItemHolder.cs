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
            case Types.SelectAndUse:
                {
                    _button.onClick.AddListener(Select);
                    break;
                }
            case Types.SimpleUse:
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
        _item.Use(this);
        BattleHandler.Instance.SetSelectedCreature(null);
        GameSession.Items.Remove(_item);
        Destroy(gameObject);
    }

    public void Select()
    {
        BattleHandler.Instance.SetItem(this);
    }

    public void TargetedUse(MonoBehaviour target)
    {
        _item.Use(target);
        GameSession.Items.Remove(_item);
        BattleHandler.Instance.SetSelectedCreature(null);
        BattleHandler.Instance.SetItemUsed(true);
        Destroy(gameObject);
    }

    public enum Types
    {
        Buying,
        Selling,
        SelectAndUse,
        SimpleUse
    }
}
