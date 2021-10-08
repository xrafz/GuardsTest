using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class BattleHandler : MonoBehaviour
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

    public int DefeatedEnemies { get; private set; } = 0;

    private Cell[,] _cells;

    public Creature SelectedCreature { get; private set; }

    public ItemHolder SelectedItem { get; private set; }

    public bool Interactable { get; private set; } = true;

    public bool ItemUsed { get; private set; } = false;

    public static BattleHandler Instance;

    public event UnityAction OnTurn;
    public event UnityAction OnWin;
    public event UnityAction OnLose;

    public delegate void DefeatedHero(Hero hero);
    public event DefeatedHero OnHeroDefeat;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Instance = this;
        if (GameSession.LocationID == GameSession.Level.Locations.Length - 1)
        {
            OnWin += () =>
            {
                GameSession.CompleteCurrentLevel();
                SceneManager.LoadScene(0);
            };
        }
        else
        {
            OnWin += () =>
            {
                GameSession.ChangeGold(GameSession.Location.GoldReward, GameSession.Location.RewardRandomness);
                GameSession.SetNextLocation();
            };
        }
        ItemUsed = false;
        OnLose += Restart;
    }

    public void SetCells(Cell[,] cells)
    {
        _cells = cells;
        _enemiesToDefeat = GameSession.Location.EnemiesToKill;
        _objective.Output(_enemiesToDefeat.ToString());
    }

    public void SetItem(ItemHolder item)
    {
        SelectedItem = item;
    }

    public void SetItemUsed(bool value)
    {
        ItemUsed = value;
    }

    public void SetSelectedCreature(Creature creature)
    {
        _unitSelector.Select(creature);
        if (Interactable)
        {
            if (SelectedCreature?.GetType() == typeof(Hero) && creature?.GetType() == typeof(Hero) && Interactable && SelectedCreature != creature)
            {
                print("swapp");
                Swap(creature);
            }
            else if (SelectedCreature == creature && creature?.GetType() == typeof(Hero))
            {
                ((Hero)creature).ChangeMode();
            }
            else
            {
                SelectedCreature = creature;
            }
        }
    }

    public void Swap(Creature creature)
    {
        var initialCell = SelectedCreature.CurrentCell;
        SelectedCreature.SetCell(creature.CurrentCell);
        creature.SetCell(initialCell);

        if (creature.CurrentCell.CellIndexes.x > SelectedCreature.CurrentCell.CellIndexes.x)
        {
            creature.GetComponent<Hero>().SetCastingStatus(true);
            print(creature);
        }
        else if (SelectedCreature.CurrentCell.CellIndexes.x > creature.CurrentCell.CellIndexes.x)
        {
            SelectedCreature.GetComponent<Hero>().SetCastingStatus(true);
            print(SelectedCreature);
        }

        SelectedCreature = null;
        SetInteractivityStatus(false);
        SetItemUsed(true);
        StartCoroutine(HeroesTurn());
    }

    public void SetInteractivityStatus(bool interactable)
    {
        Interactable = interactable;
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

        yield return new WaitForSeconds(0.5f);
        EndTurn();
    }

    private void EndTurn()
    {
        ItemUsed = false;
        OnTurn?.Invoke();

        if (WinConditionFullfilled())
        {
            HandleWin();
            return;
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

    public void HandleLose()
    {
        StopAllCoroutines();
        OnLose?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleWin()
    {
        StopAllCoroutines();
        print("Win");
        print("cur: " + GameSession.LocationID);
        OnWin?.Invoke();
    }
    
    public void AddDefeatedEnemy()
    {
        DefeatedEnemies++;
        if (DefeatedEnemies > _enemiesToDefeat)
        {
            _objective.Output("0");
        }
        else
        {
            _objective.Output((_enemiesToDefeat - DefeatedEnemies).ToString());
        }
    }

    public void SetDefeatedEnemies(int value)
    {
        DefeatedEnemies = value;
        if (DefeatedEnemies > _enemiesToDefeat)
        {
            _objective.Output("0");
        }
        else
        {
            _objective.Output((_enemiesToDefeat - DefeatedEnemies).ToString());
        }
    }

    public IEnumerator DefeatHero(Hero hero)
    {
        OnHeroDefeat?.Invoke(hero);
        yield return new WaitForSeconds(0.3f);
        if (hero.Health.Current < 1)
        {
            StopAllCoroutines();
            OnLose?.Invoke();
        }
    }

    private bool WinConditionFullfilled()
    {
        return DefeatedEnemies >= _enemiesToDefeat;
    }
}
