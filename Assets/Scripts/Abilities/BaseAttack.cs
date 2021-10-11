using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseAttack : Ability
{
    protected Creature _creature;
    protected Creature _enemy;
    protected Cell[,] _cells;
    protected int _furthestCellIndex;
    protected Transform _projectile;
    protected Animator _animator;
    protected float _attackTime;

    public override void Init(MonoBehaviour mono)
    {
        _creature = mono.GetComponent<Hero>();
        _animator = _creature.Animator;
        _projectile = _creature.Projectile?.transform;
        _cells = Field.Instance.Cells;
        SetAttackTime();
    }

    protected virtual void SetAttackTime()
    {
        _attackTime = 1f;
        var clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack")
            {
                _attackTime = clip.length;
            }
        }
    }

    protected virtual void Action()
    {
        if (!_creature.CastingAbility && _creature.AbleToMove)
        {
            if (EnemiesInAttackRange())
            {
                Attack();
            }
        }
    }

    protected virtual bool EnemiesInAttackRange()
    {
        var currentCellY = _creature.CurrentCell.CellIndexes.y;
        _furthestCellIndex = Mathf.Clamp(_creature.Data.AttackRange + _creature.CurrentCell.CellIndexes.x, 0, _cells.GetLength(1) - 1);
        for (int x = 2; x <= _furthestCellIndex; x++)
        {
            var enemy = _cells[currentCellY, x].ContainedCreature;
            if (enemy != null)
            {
                _enemy = enemy;
                return true;
            }
        }
        Debug.Log(_creature + " cant reach any enemies " + currentCellY + " " + _furthestCellIndex);
        return false;
    }

    protected virtual void Attack()
    {
        _creature.Attack();
        _creature.SetTurnTime(_attackTime);
        _animator.SetTrigger("Attack");
        _creature.Transform.DOScale(1f, _attackTime).OnComplete(() =>
        {
            if (_projectile)
            {
                Ranged();
            }
            else
            {
                Hit();
            }
        });
    }

    protected virtual void Hit()
    {
        _enemy.Hit(_creature);
        _enemy.Animator.SetTrigger("Hit");
        _enemy.Health.Change(-CalculateDamage());
        if (_enemy.Health.Current < 1)
        {
            _creature.EnemyKilled();
        }
    }

    protected virtual int CalculateDamage()
    {
        int damage = (int)((1 - (_enemy.Data.Resistance[_creature.Data.DamageType] * 0.01f)) * (_creature.Data.Damage + _creature.Data.AdditionalDamage));
        return damage;
    }

    protected void Ranged()
    {
        var position = _enemy.Transform.position;
        _projectile.position = _creature.Transform.position + (Vector3.up / 2f);
        _projectile.gameObject.SetActive(true);
        _projectile.DOMove(position, 0.2f).OnComplete(() =>
        {
            _projectile.gameObject.SetActive(false);
            Hit();
        });
    }

    public override void Sub()
    {
        Unsub();
        _creature.OnTurn += Action;
    }

    public override void Unsub()
    {
        _creature.OnTurn -= Action;
    }
}