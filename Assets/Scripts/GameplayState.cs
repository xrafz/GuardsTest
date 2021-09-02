using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : MonoBehaviour
{
    [SerializeField]
    private static int _regenValue = 10;

    private Cell[,] _cells;

    private Creature _selectedCreature;

    public Creature SelectedCreature => _selectedCreature;

    private bool _canMoveHeroes = true;

    public bool CanMoveHeroes => _canMoveHeroes;

    public static GameplayState Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCells(Cell[,] cells)
    {
        _cells = cells;
    }

    public void SetSelectedCreature(Creature creature)
    {
        
        if (_selectedCreature?.GetType() == typeof(Hero) && creature?.GetType() == typeof(Hero) && _canMoveHeroes && _selectedCreature != creature)
        {
            print("swappder");
            ChangePlaces(creature);
        }
        else
        {
            _selectedCreature = creature;
        }
    }

    public  void ChangePlaces(Creature creature)
    {
        var initialCell = _selectedCreature.CurrentCell;
        _selectedCreature.SetCell(creature.CurrentCell);
        creature.SetCell(initialCell);

        if (creature.CurrentCell.CellIndexes.x > _selectedCreature.CurrentCell.CellIndexes.x)
        {
            creature.GetComponent<Hero>().UseSpecialAbility();
            print(creature);
        }
        else if (_selectedCreature.CurrentCell.CellIndexes.x > creature.CurrentCell.CellIndexes.x)
        {
            _selectedCreature.GetComponent<Hero>().UseSpecialAbility();
            print(_selectedCreature);
        }

        _selectedCreature = null;
        SetInteractivityStatus(false);
        StartCoroutine(HeroesTurn());
    }

    public void SetInteractivityStatus(bool canMoveHeroes)
    {
        _canMoveHeroes = canMoveHeroes;
    }

    private IEnumerator HeroesTurn()
    {
        HealBackLine();
        //atk
        yield return new WaitForSeconds(1f);
        StartCoroutine(MonstersTurn());
    }

    private IEnumerator MonstersTurn()
    {
        for (int currentRow = 0; currentRow < _cells.GetLength(0); currentRow++)
        {
            for (int currentColumn = 2; currentColumn < _cells.GetLength(1); currentColumn++)
            {
                var monster = _cells[currentRow, currentColumn].ContainedCreature;
                if (monster != null)
                {
                    monster.CompleteTurn();
                    yield return new WaitForSeconds(1f);
                }
                
            }
        }
        SetInteractivityStatus(true);
    }

    private void HealBackLine()
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            if (_cells[i, 0].ContainedCreature != null)
            {
                _cells[i, 0].ContainedCreature.Health.Change(_regenValue);
                print(_cells[i, 0].ContainedCreature.name + " healed for " + _regenValue + " hp");
            }
        }
    }
}
