using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = ("Item Action/ReverseTurn"))]
public class ReverseTurn : ItemAction
{
    private TurnInfo _lastTurn;
    private int _defeatedEnemies;

    public override void Init(MonoBehaviour mono, int value)
    {
        Debug.Log("Init");
        var manager = BattleHandler.Instance;
        manager.OnLose -= manager.Restart;
        manager.OnHeroDefeat += Reverse;
        manager.OnTurn += UpdateData;
        mono.StartCoroutine(InitialUpdate());
    }

    private IEnumerator<WaitForEndOfFrame> InitialUpdate()
    {
        yield return new WaitForEndOfFrame();
        UpdateData();
    }

    public void Reverse(Hero hero)
    {
        Reverse();
    }

    private void Reverse()
    {
        var manager = BattleHandler.Instance;
        manager.SetDefeatedEnemies(_defeatedEnemies);
        manager.StopAllCoroutines();
        var creatures = _lastTurn.Creatures;
        for (int i = 0; i < creatures.Length; i++)
        {
            creatures[i].Health.Set(_lastTurn.Health[i]);
            creatures[i].Transform.DOKill();
            creatures[i].SetCell(_lastTurn.Cells[i]);
            creatures[i].Play("Idle");
        }
        manager.OnHeroDefeat -= Reverse;
        manager.OnTurn -= UpdateData;
        manager.OnLose += manager.Restart;
        manager.SetInteractivityStatus(true);
        RemoveItem();
        Destroy(this);
    }

    private void RemoveItem()
    {
        var items = GameSession.Items;
        foreach (ItemData item in items)
        {
            var actions = item.Actions;
            foreach (ItemAction action in actions)
            {
                if (action == this)
                {
                    GameSession.Items.Remove(item);
                    break;
                }
            }
        }
    }

    private void UpdateData()
    {
        _defeatedEnemies = BattleHandler.Instance.DefeatedEnemies;

        List<Creature> creatures = new List<Creature>();
        List<int> creaturesHealth = new List<int>();
        List<Cell> creaturesCells = new List<Cell>();
        var cells = Field.Instance.Cells;
        foreach (Cell cell in cells)
        {
            Creature creature = cell.ContainedCreature;
            if (creature != null)
            {
                if (creature.Health.Current < 1)
                {
                    Reverse();
                }
                creatures.Add(creature);
                creaturesHealth.Add(creature.Health.Current);
                creaturesCells.Add(creature.CurrentCell);
            }
        }
        _lastTurn = new TurnInfo(creatures.ToArray(), creaturesHealth.ToArray(), creaturesCells.ToArray());
    }

    private struct TurnInfo
    {
        public Creature[] Creatures;
        public int[] Health;
        public Cell[] Cells;

        public TurnInfo(Creature[] creatures, int[] health, Cell[] cells)
        {
            Creatures = creatures;
            Health = health;
            Cells = cells;
        }
    }
}
