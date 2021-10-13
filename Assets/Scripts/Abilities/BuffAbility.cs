using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BuffAbility")]
public class BuffAbility : Ability
{
    [SerializeField]
    private Buff[] _buffs;

    private Creature _creature;
    private int _turnsAfterBuff = 1;
    private int _turnsBetweenBuff = 2;

    public override void Init(MonoBehaviour mono)
    {
        _creature = (Creature)mono;
        _turnsBetweenBuff = ((MonsterData)_creature.Data).CastTime;
    }

    private void Action()
    {
        if (_creature.CastingAbility)
        {
            _creature.Animator.Play("Cast");
            Creature target = MobGenerator.Instance.CreatedCreatures[Random.Range(0, MobGenerator.Instance.CreatedCreatures.Count)];
            foreach (Buff effect in _buffs)
            {
                var buff = Instantiate(effect);
                buff.Init(target);
            }
            _turnsAfterBuff = 0;
            _creature.SetCastingStatus(false);
        }
    }

    private void UpdateTurns()
    {
        _turnsAfterBuff++;
        if (_turnsAfterBuff > _turnsBetweenBuff)
        {
            _creature.SetCastingStatus(true);
        }
    }

    public override void Sub()
    {
        Unsub();
        _creature.OnTurn += Action;
        BattleHandler.Instance.OnTurn += UpdateTurns;
    }

    public override void Unsub()
    {
        _creature.OnTurn -= Action;
        BattleHandler.Instance.OnTurn -= UpdateTurns;
    }
}
