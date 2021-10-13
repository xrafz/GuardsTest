using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buffs/Buff")]
public class Buff : ScriptableObject
{
    [SerializeField]
    private int _duration = 3;
    [SerializeField]
    private BuffType _type;
    [SerializeField]
    [Tooltip("Для резистов: первое значение - айди резиста, второе - на сколько поменять")]
    private int[] _value;
    [SerializeField]
    private Ability _abilityBuff;

    private int _turn = 0;
    private Creature _target;
    public void Init(Creature target)
    {
        _target = target;
        Apply();
    }

    private void Apply()
    {
        Apply(1);
        BattleHandler.Instance.OnTurn += HandleTurns;
    }

    private void Apply(int multiplier)
    {
        if (!_target)
        {
            return;
        }
        switch (_type)
        {
            case BuffType.Health:
                {
                    _target.Health.Change(_value[0] * multiplier);
                    break;
                }
            case BuffType.Damage:
                {
                    _target.Data.Upgrade(CreatureData.UpgradeTypes.Damage, _value[0] * multiplier);
                    break;
                }
            case BuffType.AbilityPower:
                {
                    _target.Data.Upgrade(CreatureData.UpgradeTypes.AbilityPower, _value[0] * multiplier);
                    break;
                }
            case BuffType.Resistance:
                {
                    _target.Data.Upgrade((CreatureData.ResistanceType)_value[0], _value[1] * multiplier);
                    break;
                }
            case BuffType.Ability:
                {
                    if (multiplier != -1)
                    {
                        _abilityBuff = Instantiate(_abilityBuff);
                        _abilityBuff.Init(_target);
                        _abilityBuff.Sub();
                    }
                    else
                    {
                        _abilityBuff.Unsub();
                    }
                    break;
                }
        }
    }

    private void HandleTurns()
    {
        _turn++;
        if (_turn > _duration)
        {
            Remove();
        }
    }

    private void Remove()
    {
        Apply(-1);
        BattleHandler.Instance.OnTurn -= HandleTurns;
        Destroy(this);
    }

    private enum BuffType
    {
        Health,
        Damage,
        AbilityPower,
        Resistance,
        Ability
    }

    private void OnValidate()
    {
        if (_type != BuffType.Ability)
        {
            _abilityBuff = null;
        }
        if (_type == BuffType.Resistance && _value.Length != 2)
        {
            _value = new int[2] { 0, 0 };
        }
        else if (_type != BuffType.Resistance && _value.Length > 1)
        {
            _value = new int[1] { 0 };
        }
    }
}
