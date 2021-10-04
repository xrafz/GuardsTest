using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Hero")]
public class HeroData : CreatureData
{
    [Header("Hero specific stats")]
    [SerializeField]
    private int[] _upgradeValues = { 0, 0, 0 };
    public int[] UpgradeValues => _upgradeValues;

    [SerializeField]
    private int[] _upgradeCosts = { 0, 0, 0 };
    public int[] UpgradeCosts => _upgradeCosts;

    [SerializeField]
    public enum UpgradeTypes
    {
        Health,
        Damage,
        AbilityPower
    }

    public void Upgrade(int index)
    {
        Upgrade(index, _upgradeValues[index]);
    }

    public void Upgrade(int index, int value)
    {
        var type = (UpgradeTypes)index;
        switch (type)
        {
            case UpgradeTypes.Health:
                {
                    ChangeHealth(value);
                    break;
                }
            case UpgradeTypes.Damage:
                {
                    ChangeDamage(value);
                    break;
                }
            case UpgradeTypes.AbilityPower:
                {
                    ChangeAbilityPower(value);
                    break;
                }
        }
        Debug.Log($"{name} upgraded {type} (+{_upgradeValues[index]})");
    }
}
