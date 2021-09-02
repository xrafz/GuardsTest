using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/HeroBashLine")]
public class BashLine : Ability
{
    [SerializeField]
    private int _stunDuration = 1;
    Cell[,] _cells;

    private Hero _hero;
    private List<Creature> _stunnedMonsters;
    private int _turnsAfterStun = 0;
    public override void Init(MonoBehaviour mono)
    {
        _hero = mono.GetComponent<Hero>();
        _stunnedMonsters = new List<Creature>();
        _hero.OnSpecialAbility += Action;
        _cells = Field.Instance.Cells;
        MonoBehaviour.print(_cells.GetLength(1));
    }

    public override void Action()
    {
        Stun();
        _hero.OnTurn += UpdateStunDuration;
    }

    private void Stun()
    {
        _turnsAfterStun = 0;
        for (int i = _hero.CurrentCell.CellIndexes.x + 1; i < _cells.GetLength(1); i++)
        {
            if (_cells[_hero.CurrentCell.CellIndexes.y, i].ContainedCreature != null)
            {
                var newMonster = _cells[_hero.CurrentCell.CellIndexes.y, i].ContainedCreature;
                MonoBehaviour.print(newMonster.name + " stunned");
                newMonster.SetAbilityToMove(false);
                _stunnedMonsters.Add(newMonster);
            }
        }
    }

    private void UpdateStunDuration()
    {
        if (_turnsAfterStun >= _stunDuration)
        {
            RemoveStun();
            return;
        }
        _turnsAfterStun++;
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
