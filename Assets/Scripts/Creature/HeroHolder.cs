using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHolder : MonoBehaviour
{
    [SerializeField]
    private HeroData _hero;

    [SerializeField]
    private GameObject _selection;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Button _button;

    private void Awake()
    {
        //установить иконку
        _icon.sprite = _hero.Sprite;
        _button.onClick.AddListener(Add);
        _selection.SetActive(false);
    }

    private void Add()
    {
        if (CreatureShop.Instance.TryAdding(_hero))
        {
            _selection.SetActive(true);
            _button.onClick.RemoveListener(Add);
            _button.onClick.AddListener(Remove);
        }
    }

    private void Remove()
    {
        _selection.SetActive(false);
        _button.onClick.RemoveListener(Remove);
        _button.onClick.AddListener(Add);

        CreatureShop.Instance.Remove(_hero);
    }

    public void SetHero(HeroData hero)
    {
        _hero = hero;
    }
}
