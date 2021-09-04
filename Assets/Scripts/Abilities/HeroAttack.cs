using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Abilities/HeroAttack")]
public class HeroAttack : Ability
{
    private Hero _hero;
    private Cell[,] _cells;
    private Monster _enemy;
    private int _rightMostCellIndex;
    private int _attackRange;

    public override void Init(MonoBehaviour mono)
    {
        _hero = mono.GetComponent<Hero>();
        _hero.OnTurn += Action;
        _cells = Field.Instance.Cells;
        _attackRange = _hero.Data.AttackRange;
    }

    public override void Action()
    {
        if (!_hero.UsingSpecialAbility && _hero.AbleToMove)
        {
            if (EnemiesInAttackRange())
            {
                Attack();
            }
        }
    }

    private bool EnemiesInAttackRange()
    {
        var currentCellY = _hero.CurrentCell.CellIndexes.y;
        _rightMostCellIndex = Mathf.Clamp(_attackRange + _hero.CurrentCell.CellIndexes.x, 0, _cells.GetLength(1) - 1);
        Debug.Log(_hero.name + " " + _attackRange + " " + _hero.CurrentCell.CellIndexes.x + " " + (_attackRange + _hero.CurrentCell.CellIndexes.x));
        for (int x = 2; x <= _rightMostCellIndex; x++)
        {
            var enemy = _cells[currentCellY, x].ContainedCreature;
            if (enemy != null)
            {
                _enemy = (Monster)enemy;
                return true;
            }
        }
        Debug.Log(_hero + " cant reach any enemies " + currentCellY + " " + _rightMostCellIndex);
        return false;
    }

    private void Attack()
    {
        _hero.Transform.DOScale(1.3f, 0.3f).OnComplete(() =>
        {
            _enemy.Transform.DOShakeScale(0.2f);
            _hero.Transform.DOScale(1f, 0.3f);
        });
        _enemy.Health.Change(-_hero.Data.Damage);
        Debug.Log(string.Format("{0} attacked {1}, dealing {2} damage", _hero, _enemy, _hero.Data.Damage));
    }
}
