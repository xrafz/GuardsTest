using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Item Action/ChangeStat"))]
public class ChangeStat : ItemAction
{
    [SerializeField]
    private int _statIndex;
    [SerializeField]
    private int _duration;

    private Creature _hero;
    private int _buffValue;
    private int _turnsAfterBuff = 0;

    public override void Init(MonoBehaviour mono, int value)
    {
        _hero = mono.GetComponent<Hero>();
        _buffValue = value;
        ((HeroData)_hero.Data).Upgrade(_statIndex, _buffValue);
        BattleState.Instance.OnTurn += UpdateTurns;
        BattleState.Instance.OnWin += Debuff;
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
        ((HeroData)_hero.Data).Upgrade(_statIndex, -_buffValue);
        BattleState.Instance.OnWin -= Debuff;
        BattleState.Instance.OnTurn -= UpdateTurns;
    }

    private void OnValidate()
    {
        var maxLength = Enum.GetNames(typeof(HeroData.UpgradeTypes)).Length;
        _statIndex = Mathf.Clamp(_statIndex, 1, maxLength);
    }
}
