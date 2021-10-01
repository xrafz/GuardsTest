using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Hero")]
public class HeroData : CreatureData
{
    [Header("Hero specific stats")]
    [SerializeField]
    private int[] _upgradeValue;

    public enum UpgradeTypes
    {
        Health,
        Damage,
        AbilityPower
    }
}
