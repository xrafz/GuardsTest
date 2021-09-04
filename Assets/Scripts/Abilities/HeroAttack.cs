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
    private Transform _projectile;

    public override void Init(MonoBehaviour mono)
    {
        _hero = mono.GetComponent<Hero>();
        _projectile = _hero.Projectile?.transform;
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
            if (_projectile)
            {
                Ranged();
            }
            else
            {
                Shake();
            }
        });
        _enemy.Health.Change(-_hero.Data.Damage);
    }

    private void Shake()
    {
        _enemy.Transform.DOShakeScale(0.2f);
        _hero.Transform.DOScale(1f, 0.3f);
    }

    private void Ranged()
    {
        var position = _enemy.Transform.position;
        _projectile.position = _hero.Transform.position + (Vector3.up / 2f);
        _projectile.gameObject.SetActive(true);
        _projectile.DOMove(position, 0.2f).OnComplete(() =>
        {
            _projectile.gameObject.SetActive(false);
            Shake();
        });
    }
}
