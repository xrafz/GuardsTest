using UnityEngine;
using DG.Tweening;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/MonsterMoveOrAttack")]
public class MonsterMoveOrAttack : Ability
{
    [SerializeField]
    private int _moveDistance = 1;

    private Creature _creature;
    private Creature _enemy;
    private Cell[,] _cells;
    private int _furthestCellIndex;
    private Transform _projectile;
    private Animator _animator;
    private float _attackTime;
    private int _passedSteps;

    private int _currentCellX, _currentCellY;

    public override void Init(MonoBehaviour mono)
    {
        _creature = mono.GetComponent<Monster>();
        _animator = _creature.Animator;
        _projectile = _creature.Projectile?.transform;
        _cells = Field.Instance.Cells;
        _moveDistance = ((MonsterData)_creature.Data).MovementDistance;

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
        if (_creature.AbleToMove && !_creature.CastingAbility)
        {
            if (EnemiesInAttackRange())
            {
                Attack();
            }
            else
            {
                _passedSteps = 0;
                for (int currentSteps = 0; currentSteps < _moveDistance; currentSteps++)
                {
                    Move();
                }
                if (_passedSteps != 0)
                {
                    _creature.StartCoroutine(WaitForMovement());
                }
            }
        }
    }

    private IEnumerator WaitForMovement()
    {
        var timeToWait = _passedSteps * 0.8f;
        _creature.Animator.SetTrigger("Walk");
        _creature.SetTurnTime(timeToWait);
        yield return new WaitForSeconds(timeToWait);
        _creature.Animator.SetTrigger("StopWalking");
    }

    private void Move()
    {
        if (_cells[_currentCellY, _currentCellX - 1].ContainedCreature == null)
        {
            _passedSteps++;
            _creature.CurrentCell.SetContainedCreature(null);
            _creature.SetCell(_cells[_currentCellY, _currentCellX - 1], 0.8f * _passedSteps);
            CalculateCurrentCell();
        }
        else
        {
            Debug.Log(_creature.name + " cant get over teammate/enemy");
        }
    }

    private void CalculateCurrentCell()
    {
        _currentCellX = _creature.CurrentCell.CellIndexes.x;
        _currentCellY = _creature.CurrentCell.CellIndexes.y;
    }

    private bool EnemiesInAttackRange()
    {
        CalculateCurrentCell();
        _furthestCellIndex = Mathf.Clamp(_currentCellX - _creature.Data.AttackRange, 0, _currentCellX);
        for (int i = _currentCellX; i >= _furthestCellIndex; i--)
        {
            var creature = _cells[_currentCellY, i].ContainedCreature;
            if (creature != null)
            {
                if (creature.GetType() == typeof(Hero))
                {
                    _enemy = creature;
                    return true;
                }
            }
        }
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
        MonoBehaviour.print(_creature.name + " attacked " + _enemy.name + ", dealing " + _creature.Data.Damage + " damage");
    }

    private void Shake()
    {
        _enemy.Animator.SetTrigger("Hit");
        _enemy.Health.Change(-_creature.Data.Damage);
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
