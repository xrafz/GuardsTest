using UnityEngine;
using DG.Tweening;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/MonsterMoveOrAttack")]
public class MonsterMoveOrAttack : Ability
{
    private Monster _monster;
    private Animator _animator;
    private Cell[,] _cells;
    private Hero _enemy;
    private Transform _projectile;

    private int _currentCellX, _currentCellY;

    public override void Init(MonoBehaviour mono)
    {
        _monster = mono.GetComponent<Monster>();
        _animator = _monster.Animator;
        _projectile = _monster.Projectile?.transform;
        _cells = Field.Instance.Cells;
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
            _monster.SetCell(_cells[_currentCellY, _currentCellX - 1], 0.8f);
        }
        else 
            Debug.Log(_monster.name + " cant get over teammate");
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
        _animator.Play("Attack");
        _monster.Transform.DOScale(1f, _animator.GetCurrentAnimatorStateInfo(0).length).OnComplete(() =>
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
        MonoBehaviour.print(_monster.name + " attacked " + _enemy.name + ", dealing " + _monster.Data.Damage + " damage");
    }

    private void Shake()
    {
        //_enemy.Transform.DOShakeScale(0.2f);
        //_monster.Transform.DOScale(1f, 0.3f);
        _enemy.Animator.Play("Hit");
        _enemy.Health.Change(-_monster.Data.Damage);
    }

    private void Ranged()
    {
        var position = _enemy.Transform.position;
        _projectile.position = _monster.Transform.position + (Vector3.up / 2f);
        _projectile.gameObject.SetActive(true);
        _projectile.DOMove(position, 0.2f).OnComplete(() =>
        {
            _projectile.gameObject.SetActive(false);
            Shake();
        });
    }
}
