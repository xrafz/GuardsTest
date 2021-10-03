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
        var type = (UpgradeTypes)index;
        switch (type)
        {
            case UpgradeTypes.Health:
                {
                    ChangeHealth(_upgradeValues[index]);
                    break;
                }
            case UpgradeTypes.Damage:
                {
                    ChangeDamage(_upgradeValues[index]);
                    break;
                }
            case UpgradeTypes.AbilityPower:
                {
                    ChangeAbilityPower(_upgradeValues[index]);
                    break;
                }
        }
        Debug.Log($"{name} upgraded {type} (+{_upgradeValues[index]})");
    }
}
