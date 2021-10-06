using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Abilities/HeroAttack")]
public class HeroAttack : Ability
{
    private Creature _creature;
    private Creature _enemy;
    private Cell[,] _cells;
    private int _furthestCellIndex;
    private Transform _projectile;
    private Animator _animator;
    private float _attackTime;

    public override void Init(MonoBehaviour mono)
    {
        _creature = mono.GetComponent<Hero>();
        _animator = _creature.Animator;
        _projectile = _creature.Projectile?.transform;
        _cells = Field.Instance.Cells;

        _attackTime = 1f;
        var clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack")
            {
                _attackTime = clip.length;
            }
        }

        _creature.OnTurn += Action;
    }

    private void Action()
    {
        if (!_creature.CastingAbility && _creature.AbleToMove)
        {
            if (EnemiesInAttackRange())
            {
                Attack();
            }
        }
    }

    private bool EnemiesInAttackRange()
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

    private void Attack()
    {
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
                Shake();
            }
        });
        
    }

    private void Shake()
    {
        _enemy.Animator.SetTrigger("Hit");
        int damage = (int)((1 - (_enemy.Data.Resistance[(int)_creature.Data.DamageType] * 0.01f)) * _creature.Data.Damage);
        _enemy.Health.Change(-damage);
    }

    private void Ranged()
    {
        var position = _enemy.Transform.position;
        _projectile.position = _creature.Transform.position + (Vector3.up / 2f);
        _projectile.gameObject.SetActive(true);
        _projectile.DOMove(position, 0.2f).OnComplete(() =>
        {
            _projectile.gameObject.SetActive(false);
            Shake();
        });
    }
}
