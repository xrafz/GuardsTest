using UnityEngine;
using DG.Tweening;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/MoveOrAttack")]
public class MoveOrAttack : BaseAttack
{
    [SerializeField]
    protected int _moveDistance = 1;
    protected int _currentCellX, _currentCellY;
    protected int _passedSteps;

    public override void Init(MonoBehaviour mono)
    {
        _creature = mono.GetComponent<Monster>();
        _animator = _creature.Animator;
        _projectile = _creature.Projectile?.transform;
        _cells = Field.Instance.Cells;
        _moveDistance = ((MonsterData)_creature.Data).MovementDistance;
        SetAttackTime();
    }

    protected override void Action()
    {
        if (_creature.AbleToMove && !_creature.CastingAbility)
        {
            if (EnemiesInAttackRange())
            {
                Attack();
            }
            else
            {
                _creature.StartCoroutine(HandleMovement());
            }
        }
    }

    protected virtual IEnumerator HandleMovement()
    {
        _passedSteps = 0;
        for (int currentSteps = 0; currentSteps < _moveDistance; currentSteps++)
        {
            MakeStep();
        }
        if (_passedSteps != 0)
        {
            var timeToWait = _passedSteps * 0.8f;
            _creature.Animator.SetTrigger("Walk");
            _creature.SetTurnTime(timeToWait);
            yield return new WaitForSeconds(timeToWait);
            _creature.Animator.SetTrigger("StopWalking");
        }
    }

    protected void MakeStep()
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

    protected void CalculateCurrentCell()
    {
        _currentCellX = _creature.CurrentCell.CellIndexes.x;
        _currentCellY = _creature.CurrentCell.CellIndexes.y;
    }

    protected override bool EnemiesInAttackRange()
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
}
