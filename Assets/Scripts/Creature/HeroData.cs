using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Hero")]
public class HeroData : CreatureData
{
    [Header("Hero specific stats")]
    [SerializeField]
    [Tooltip("0 - ��, 1 - ����, 2 - ���� �����������")]
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
