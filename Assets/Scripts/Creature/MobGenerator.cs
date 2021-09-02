using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator : CreatureGenerator<Monster>
{
    [SerializeField]
    private int _minimalDistance;

    public static MobGenerator Instance;

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
        }
    }

    protected override void SetData()
    {
        foreach (Creature creature in _createdCreatures)
        {
            creature.SetData(_creatures[Random.Range(0, _creatures.Length)]);
        }
    }

    private void Start()
    {
        Instance = this;
    }
}
