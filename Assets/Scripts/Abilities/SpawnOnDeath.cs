using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SpawnOnDeath")]
public class SpawnOnDeath : Ability
{
    [SerializeField]
    private int _minimumX = 3;
    private Creature _creature;

    public override void Init(MonoBehaviour mono)
    {
        _creature = mono.GetComponent<Creature>();
    }

    private void AddToTurn()
    {
        BattleHandler.Instance.OnTurn += Action;
    }

    private void Action()
    {
        Cell desiredCell = RandomCell();
        if (desiredCell != null)
        {
            var creature = MobGenerator.Instance.GenerateCreature();
            creature.SetCell(desiredCell);
        }

        MobGenerator.Instance.CreatedCreatures.Remove((Monster)_creature);
        _creature.Health.OnDeath -= Action;
        BattleHandler.Instance.OnTurn -= AddToTurn;
    }

    private Cell RandomCell()
    {
        Cell[,] cells = Field.Instance.Cells;
        Cell cell;
        do
        {
            cell = cells[Random.Range(0, cells.GetLength(0)), Random.Range(_minimumX, cells.GetLength(1))];
        } while (cell.ContainedCreature != null);
        return cell;
    }

    public override void Sub()
    {
        Unsub();
        _creature.Health.OnDeath += AddToTurn;
    }

    public override void Unsub()
    {
        _creature.Health.OnDeath -= AddToTurn;
    }
}
