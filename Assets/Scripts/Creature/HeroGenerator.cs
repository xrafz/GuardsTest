using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGenerator : CreatureGenerator
{ 
    protected override void ChangePositions()
    {
        var cells = Field.Instance.Cells;

        Cell[] startingCells = new Cell[] //��������� ������� ������
        {
            cells[0, 1], cells[1, 1], cells[2, 1], cells[1, 1], cells[0, 1], cells[2, 1]
        };

        for (int i = 0; i < _createdCreatures.Count; i++)
        {
            _createdCreatures[i].SetCell(startingCells[i]);
            startingCells[i].SetContainedCreature(_createdCreatures[i]);
        }
    }
}
