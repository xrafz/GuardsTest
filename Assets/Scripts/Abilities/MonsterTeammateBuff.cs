using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Abilities/MonsterBuffTeammate")]
public class MonsterTeammateBuff : Ability
{
    [SerializeField]
    private int _abilityPower;
    [SerializeField]
    private int _turnsBeforeBuff = 2; //if =2, then buff every 3rd turn

    private int _turnsAfterBuff;
    private Monster _monster;
    public override void Init(MonoBehaviour mono)
    {
        _monster = mono.GetComponent<Monster>();
        _abilityPower = _monster.Data.AbilityPower;
        _turnsBeforeBuff = ((MonsterData)_monster.Data).CastTime;
        _turnsAfterBuff = 1;
    }

    private void Action()
    {
        if (_monster.CastingAbility)
        {
            BuffSomeOne();
        }
    }

    private void BuffSomeOne()
    {
        var monsters = MobGenerator.Instance.CreatedCreatures;
        Monster selectedMonster;
        do
        {
            selectedMonster = monsters[Random.Range(0, monsters.Count)];
        } while (selectedMonster == _monster);
        selectedMonster.Data.ChangeDamage(_abilityPower);
        Debug.Log("Added damage to " + selectedMonster.name);
        _monster.Transform.DOScaleY(2f, 0.5f).OnComplete(() =>
        {
            _monster.Transform.DOScaleY(1f, 0.4f);
            //selectedMonster.Transform.DOShakeScale(0.4f);
            selectedMonster.Transform.DOScale(1.2f, 0.4f);
        });

        _turnsAfterBuff = -1;
        _monster.SetCastingStatus(false);
    }

    private void UpdateTurns()
    {
        _turnsAfterBuff++;
        if (_turnsAfterBuff >= _turnsBeforeBuff)
        {
            _monster.SetCastingStatus(true);
        }
    }

    public override void Sub()
    {
        Unsub();
        BattleHandler.Instance.OnTurn += UpdateTurns;
        _monster.OnTurn += Action;
    }

    public override void Unsub()
    {
        BattleHandler.Instance.OnTurn -= UpdateTurns;
        _monster.OnTurn -= Action;
    }
}
