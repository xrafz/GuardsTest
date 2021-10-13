using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Item Action/ChangeStat"))]
public class ChangeStat : ItemAction
{
    [SerializeField]
    [Tooltip("0 - хп, 1 - урон, 2 - сила способности")]
    private CreatureData.UpgradeTypes _stat;
    [SerializeField]
    private int _duration;

    private Creature _hero;
    private int _buffValue;
    private int _turnsAfterBuff = 0;

    public override void Init(MonoBehaviour mono, int value)
    {
        _hero = mono.GetComponent<Hero>();
        _buffValue = value;
        ((HeroData)_hero.Data).Upgrade(_stat, _buffValue);
        BattleHandler.Instance.OnTurn += UpdateTurns;
        BattleHandler.Instance.OnWin += Debuff;
        Debug.Log($"Buffed {_hero.name} for {_buffValue}");
    }

    private void UpdateTurns()
    {
        _turnsAfterBuff++;
        if (_turnsAfterBuff >= _duration)
        {
            Debuff();
        }
    }

    private void Debuff()
    {
        ((HeroData)_hero.Data).Upgrade(_stat, -_buffValue);
        BattleHandler.Instance.OnWin -= Debuff;
        BattleHandler.Instance.OnTurn -= UpdateTurns;
    }

    private void OnValidate()
    {
        if (_stat == CreatureData.UpgradeTypes.Health)
        {
            _stat = CreatureData.UpgradeTypes.Damage;
        }
    }
}
