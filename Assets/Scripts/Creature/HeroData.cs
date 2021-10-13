using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Hero")]
public class HeroData : CreatureData
{
    [Header("Hero specific stats")]
    [SerializeField]
    [Tooltip("0 - хп, 1 - урон, 2 - сила способности")]
    private int[] _upgradeValues = { 0, 0, 0 };
    public int[] UpgradeValues => _upgradeValues;

    [SerializeField]
    private int[] _upgradeCosts = { 0, 0, 0 };
    public int[] UpgradeCosts => _upgradeCosts;

    [SerializeField]
    private Ability[] _firstModeAbilities;
    public Ability[] FirstModeAbilities => _firstModeAbilities;

    [SerializeField]
    private Ability[] _secondModeAbilities;
    public Ability[] SecondModeAbilities => _secondModeAbilities;

    [SerializeField]
    private int _firstModeDamageValue;
    public int FirstModeDamageValue => _firstModeDamageValue;

    [SerializeField]
    private int _firstModeDamageType;
    public int FirstModeDamageType => _firstModeDamageType;

    [SerializeField]
    private int _secondModeDamageValue;
    public int SecondModeDamageValue => _secondModeDamageValue;

    [SerializeField]
    private int _secondModeDamageType;
    public int SecondModeDamageType => _secondModeDamageType;

    [SerializeField]
    private AnimatorOverrideController _secondModeAnimator;
    public AnimatorOverrideController SecondModeAnimator => _secondModeAnimator;

    public void Upgrade(UpgradeTypes type)
    {
        Upgrade(type, _upgradeValues[(int)type]);
    }
}
