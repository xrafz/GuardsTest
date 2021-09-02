using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator : CreatureGenerator
{
    [SerializeField]
    private int _minimalDistance;

    public override void InitialGeneration()
    {
        base.InitialGeneration();
        Reshuffle();
    }

    protected override void ChangePositions()
    {
        var cells = Field.Instance.Cells;
        List<Cell> selectedCells = new List<Cell>();
        for (int i = 0; i < _createdCreatures.Count; i++)
        {
            Cell randomCell;
            do
            {
                randomCell = cells[Random.Range(0, cells.GetLength(0)), Random.Range(_minimalDistance, cells.GetLength(1))]; //ставим моба на клетку, где никого нет
            } while(selectedCells.Contains(randomCell));

            selectedCells.Add(randomCell);
            _createdCreatures[i].SetCell(randomCell);
            //randomCell.SetContainedCreature(_createdCreatures[i]);
        }
    }

    private void Reshuffle()
    {
        foreach (Creature creature in _createdCreatures)
        {
            creature.SetData(_creatures[Random.Range(0, _creatures.Length)]);
        }
    }
}
