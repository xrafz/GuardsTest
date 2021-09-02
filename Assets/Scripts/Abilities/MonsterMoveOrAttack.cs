using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MonsterMoveOrAttack")]
public class MonsterMoveOrAttack : Ability
{
    private Monster _monster;
    private Cell[,] _cells;
    private int _cellsX, _cellsY;
    private Hero _enemy;

    private int _currentCellX, _currentCellY;

    public override void Init(MonoBehaviour mono)
    {
        _monster = mono.GetComponent<Monster>();
        _cells = Field.Instance.Cells;
        _cellsX = _cells.GetLength(1);
        _cellsY = _cells.GetLength(0);
        _monster.OnTurn -= Action;
        _monster.OnTurn += Action;
    }

    public override void Action()
    {
        if (_monster.AbleToMove && !_monster.CastingAbility)
        {
            if (EnemiesInAttackRange())
            {
                Attack();
            }
            else
            {
                Move();
            }
        }
    }

    private void Move()
    {
        if (_cells[_currentCellY, _currentCellX - 1].ContainedCreature == null)
        {
            _monster.CurrentCell.SetContainedCreature(null);
            _monster.SetCell(_cells[_currentCellY, _currentCellX - 1]);
        }
        MonoBehaviour.print(_monster.name + " cant get over teammate");
    }

    private void CalculateCurrentCell()
    {
        _currentCellX = _monster.CurrentCell.CellIndexes.x;
        _currentCellY = _monster.CurrentCell.CellIndexes.y;
    }

    private bool EnemiesInAttackRange()
    {
        CalculateCurrentCell();
        var leftMostCellIndex = Mathf.Clamp(_currentCellX - _monster.Data.AttackRange, 0, _currentCellX);
        for (int i = _currentCellX; i >= leftMostCellIndex; i--)
        {
            var creature = _cells[_currentCellY, i].ContainedCreature;
            if (creature != null)
            {
                if (creature.GetType() == typeof(Hero))
                {
                    _enemy = (Hero)creature;
                    return true;
                }
            }
        }
        return false;
    }

    private void Attack()
    {
        _enemy.Health.Change(-_monster.Data.Damage);
        MonoBehaviour.print(_monster.name + " attacked " + _enemy.name + ", dealing " + _monster.Data.Damage + " damage");
    }
}
