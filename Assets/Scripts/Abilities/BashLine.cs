using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/HeroBashLine")]
public class BashLine : Ability
{
    [SerializeField]
    private int _stunDuration = 1;
    [SerializeField]
    private int _stunDamage = 10;
    Cell[,] _cells;

    private Hero _hero;
    private List<Creature> _stunnedMonsters;
    private int _turnsAfterStun = 0;
    public override void Init(MonoBehaviour mono)
    {
        _hero = mono.GetComponent<Hero>();
        _stunnedMonsters = new List<Creature>();
        _hero.OnTurn += Action;
        _cells = Field.Instance.Cells;
    }

    public override void Action()
    {
        if (_hero.UsingSpecialAbility)
        {
            RemoveStun();
            Stun();
            _hero.OnTurn -= Action;
            _hero.OnTurn += UpdateStunDuration;
            _hero.OnTurn += Action;
        }
    }

    private void Stun()
    {
        _turnsAfterStun = 0;
        for (int i = _hero.CurrentCell.CellIndexes.x + 1; i < _cells.GetLength(1); i++)
        {
            if (_cells[_hero.CurrentCell.CellIndexes.y, i].ContainedCreature != null)
            {
                var newMonster = _cells[_hero.CurrentCell.CellIndexes.y, i].ContainedCreature;
                newMonster.Health.Change(-_stunDamage);
                MonoBehaviour.print(newMonster.name + " stunned");
                newMonster.SetAbilityToMove(false);
                _stunnedMonsters.Add(newMonster);
            }
        }
    }

    private void UpdateStunDuration()
    {
        _hero.SetSpecialAbilityStatus(false);
        _turnsAfterStun++;
        if (_turnsAfterStun >= _stunDuration)
        {
            RemoveStun();
            return;
        }
    }

    private void RemoveStun()
    {
        foreach (Creature creature in _stunnedMonsters)
        {
            creature.SetAbilityToMove(true);
        }
        _stunnedMonsters.Clear();
        _turnsAfterStun = 0;
        _hero.OnTurn -= UpdateStunDuration;
    }
}
