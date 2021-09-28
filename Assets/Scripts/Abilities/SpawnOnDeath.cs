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
        _creature.Health.OnDeath += Action;
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
        Destroy(_creature.gameObject);
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
}
