using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGenerator : CreatureGenerator<Hero>
{
    private int _currentIndex = 0;
    protected override void ChangePositions()
    {
        var cells = Field.Instance.Cells;

        Cell[] startingCells = new Cell[] //начальные позиции героев
        {
            cells[0, 1], cells[1, 1], cells[2, 1], cells[1, 0], cells[0, 1], cells[2, 1]
        };

        for (int i = 0; i < _createdCreatures.Count; i++)
        {
            _createdCreatures[i].SetCell(startingCells[i]);
        }
        _currentIndex = 0;
    }

    protected override void SetData(Creature creature)
    {
        creature.SetData(_creatures[_currentIndex]);
        _currentIndex++;
    }
}
