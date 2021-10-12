using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Poison")]
public class Poison : Ability
{
    [SerializeField]
    private int _poisonDuration;

    private Creature _creature;
    private int _turnsBeforePoisoning;
    private int _turnsAfterPoisoning = 0;
    private int _turnsAfterApplying = 0;
    private Creature _enemy;
    private bool _ableToPoison;

    public override void Init(MonoBehaviour mono)
    {
        _creature = (Creature)mono;
        _turnsBeforePoisoning = ((MonsterData)_creature.Data).CastTime;
    }

    private void Action()
    {
        if (_creature.CastingAbility)
        {
            if (EnemiesInAttackRange())
            {
                _enemy.OnTurn += ApplyPoison;
                _turnsAfterApplying = 0;
                _creature.Animator.Play("Cast");
                _ableToPoison = false;
            }
            _creature.SetCastingStatus(false);
        }
    }

    private void ApplyPoison()
    {
        _turnsAfterApplying++;
        _enemy?.Health.Change(-((MonsterData)_creature.Data).AbilityPower);
        Debug.Log(_enemy.name);
        if (_turnsAfterApplying >= _poisonDuration)
        {
            RemovePoison();
        }
    }

    private void RemovePoison()
    {
        _enemy.OnTurn -= ApplyPoison;
        _turnsAfterApplying = 0;
        _ableToPoison = true;
    }

    private bool EnemiesInAttackRange()
    {
        var indexes = _creature.CurrentCell.CellIndexes;
        var cells = Field.Instance.Cells;
        var furthestCellIndex = Mathf.Clamp(indexes.x - _creature.Data.AttackRange, 0, indexes.x);
        for (int i = indexes.x ; i >= furthestCellIndex; i--)
        {
            var creature = cells[indexes.y, i].ContainedCreature;
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
    private void UpdateTurns()
    {
        _turnsAfterPoisoning++;
        if (_ableToPoison && _turnsAfterPoisoning > _turnsBeforePoisoning)
        {
            _creature.SetCastingStatus(true);
        }
    }

    public override void Sub()
    {
        Unsub();
        BattleHandler.Instance.OnTurn += UpdateTurns;
        _creature.OnTurn += Action;
        _ableToPoison = true;
        _turnsAfterPoisoning = 0;
    }

    public override void Unsub()
    {
        BattleHandler.Instance.OnTurn -= UpdateTurns;
        _creature.OnTurn -= Action;
    }
}
