using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Hero")]
public class HeroData : CreatureData
{
    [Header("Hero specific stats")]
    [SerializeField]
    private int[] _upgradeValues;
    public int[] UpgradeValues => _upgradeValues;

    [SerializeField]
    private int[] _upgradeCosts;
    public int[] UpgradeCosts => _upgradeCosts;

    public enum UpgradeTypes
    {
        Health,
        Damage,
        AbilityPower
    }
}
