using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleState : MonoBehaviour
{
    [SerializeField]
    private int _regenValue = 10;

    [SerializeField]
    private int _enemiesToDefeat = 12;

    [SerializeField]
    private UnitSelector _unitSelector;

    [SerializeField]
    private TextOutput _objective;

    [SerializeField]
    private YourTurnNotification _yourTurnNotifier;

    private int _defeatedEnemies = 0;

    private Cell[,] _cells;

    public Creature SelectedCreature { get; private set; }

    public bool CanMoveHeroes { get; private set; } = true;

    public static BattleState Instance;

    public delegate void Blank();
    public event Blank OnTurn;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCells(Cell[,] cells)
    {
        _cells = cells;
        _enemiesToDefeat = GameSession.CurrentLocation.EnemiesToKill;
        _objective.Output(_enemiesToDefeat.ToString());
    }

    public void SetSelectedCreature(Creature creature)
    {
        _unitSelector.Select(creature);
        if (SelectedCreature?.GetType() == typeof(Hero) && creature?.GetType() == typeof(Hero) && CanMoveHeroes && SelectedCreature != creature)
        {
            print("swapp");
            ChangePlaces(creature);
        }
        else
        {
            SelectedCreature = creature;
        }
    }

    public void ChangePlaces(Creature creature)
    {
        var initialCell = SelectedCreature.CurrentCell;
        SelectedCreature.SetCell(creature.CurrentCell);
        creature.SetCell(initialCell);

        if (creature.CurrentCell.CellIndexes.x > SelectedCreature.CurrentCell.CellIndexes.x)
        {
            creature.GetComponent<Hero>().SetSpecialAbilityStatus(true);
            print(creature);
        }
        else if (SelectedCreature.CurrentCell.CellIndexes.x > creature.CurrentCell.CellIndexes.x)
        {
            SelectedCreature.GetComponent<Hero>().SetSpecialAbilityStatus(true);
            print(SelectedCreature);
        }

        SelectedCreature = null;
        SetInteractivityStatus(false);
        StartCoroutine(HeroesTurn());
    }

    public void SetInteractivityStatus(bool canMoveHeroes)
    {
        CanMoveHeroes = canMoveHeroes;
    }

    private IEnumerator HeroesTurn()
    {
        HealBackLine();
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            yield return new WaitForSeconds(_cells[i, 1].ContainedCreature.CompleteTurn());
        }

        if (WinConditionFullfilled())
        {
            HandleWin();
            yield break;
        }

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
                    yield return new WaitForSeconds(monster.CompleteTurn());
                }

            }
        }
        EndTurn();
    }

    private void EndTurn()
    {
        OnTurn?.Invoke();

        if (WinConditionFullfilled())
        {
            HandleWin();
            return;
        }

        _yourTurnNotifier.Notify();
        SetInteractivityStatus(true);
        print("turn ended");
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

    public void HandleLose()
    {
        StopAllCoroutines();
        print("Lose");

        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        /*
        _selectedCreature = null;
        _defeatedEnemies = 0;
        SetInteractivityStatus(true);
        _objective.Output(_enemiesToDefeat.ToString());
        Field.Instance.InitSpawners();
        */
    }

    public void HandleWin()
    {
        StopAllCoroutines();
        print("Win");
        print("cur: " + GameSession.CurrentLocationID);
        if(GameSession.CurrentLocationID == GameSession.CurrentLevel.Locations.Length - 1)
        {
            //сохранить в файлик
            SceneManager.LoadScene(0);
        }
        else
        {
            Restart();
            GameSession.SetNextLocation();
        }
        //?
    }
    
    public void AddDefeatedEnemy()
    {
        _defeatedEnemies++;
        print("defeated " + _defeatedEnemies + " enemies");

        if (_defeatedEnemies > _enemiesToDefeat)
        {
            _objective.Output("0");
        }
        else
        {
            _objective.Output((_enemiesToDefeat - _defeatedEnemies).ToString());
        }
    }

    private bool WinConditionFullfilled()
    {
        return _defeatedEnemies >= _enemiesToDefeat;
    }
}
