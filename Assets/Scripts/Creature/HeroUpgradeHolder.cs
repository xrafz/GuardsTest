using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroUpgradeHolder : MonoBehaviour
{
    [SerializeField]
    private HeroData _hero;
    [SerializeField]
    private TMP_Text _valueText, _costText;
    [SerializeField]
    private Image _image;

    private int _value, _cost;
    private void OnEnable()
    {
        _image.sprite = _hero.Sprite;
    }

    public void Init(HeroData hero)
    {
        _hero = hero;
        var statIndex = Random.Range(0, _hero.UpgradeValues.Length);
        _value = _hero.UpgradeValues[statIndex];
        _valueText.text = $"{_value}";
        _cost = _hero.UpgradeCosts[statIndex];
        _costText.text = $"{_cost}";
        gameObject.SetActive(true);
    }

    //эвент для изменения суммы в сессии?
}
