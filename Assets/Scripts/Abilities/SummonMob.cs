using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SummonMob")]
public class SummonMob : Ability
{
    [SerializeField]
    private CreatureData _creatureToSpawn;
    [SerializeField]
    private bool _casterIsMonster = true;
    [SerializeField]
    private int _turnsBeforeSummoning = 2;

    private int _turnsAfterSummoning = 0;
    private Creature _creature;
    private Cell[,] _cells;
    public override void Init(MonoBehaviour mono)
    {
        _creature = (Monster)mono;
        _turnsBeforeSummoning = ((MonsterData)_creature.Data).CastTime;
        _cells = Field.Instance.Cells;
    }

    private void Action()
    {
        if (_creature.CastingAbility && _creature.AbleToMove)
        {
            var nextCellIndexes = _creature.CurrentCell.CellIndexes;
            if (_casterIsMonster)
            {
                nextCellIndexes.x -= 1;
            }
            else
            {
                nextCellIndexes.x += 1;
            }
            if (NextCellEmpty(nextCellIndexes))
            {
                var mob = MobGenerator.Instance.GenerateCreature(_creatureToSpawn);
                mob.SetData(_creatureToSpawn);
                mob.Init();
                mob.SetCell(_cells[nextCellIndexes.y, nextCellIndexes.x]);
            }
            _turnsAfterSummoning = 0;
            _creature.SetCastingStatus(false);
        }
    }

    private bool NextCellEmpty(Vector2Int indexes)
    {
        if (_cells[indexes.y, indexes.x].ContainedCreature == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateTurns()
    {
        if (_casterIsMonster)
        {
            _turnsAfterSummoning++;
            if (_turnsAfterSummoning >= _turnsBeforeSummoning)
            {
                _creature.SetCastingStatus(true);
            }
        }
    }

    public override void Sub()
    {
        Unsub();
        _creature.OnTurn += Action;
        BattleHandler.Instance.OnTurn += UpdateTurns;
    }

    public override void Unsub()
    {
        _creature.OnTurn -= Action;
        BattleHandler.Instance.OnTurn -= UpdateTurns;
    }
}
