using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroUpgradeHolder : MonoBehaviour
{
    [SerializeField]
    private HeroData _hero;
    public HeroData Hero => _hero;

    [SerializeField]
    private TMP_Text _valueText, _costText;
    [SerializeField]
    private Image _image;

    private int _value, _cost, _statIndex;

    public void Init(HeroData hero)
    {
        _hero = hero;
        _statIndex = Random.Range(0, _hero.UpgradeValues.Length);
        _value = _hero.UpgradeValues[_statIndex];
        _valueText.text = $"{_value}";
        _cost = _hero.UpgradeCosts[_statIndex];
        _costText.text = $"{_cost}";
        gameObject.SetActive(true);
        _image.sprite = _hero.Sprite;
    }

    public void Upgrade()
    {
        if (GameSession.Gold - _cost >= 0)
        {
            _hero.Upgrade(_statIndex);
            GameSession.ChangeGold(-_cost);
        }
    }
}
