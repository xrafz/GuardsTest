using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Item Action/ReverseTurn"))]
public class ReverseTurn : ItemAction
{
    private List<GameObject> _creatures = new List<GameObject>();
    [SerializeField]
    private GameObject _prefab;

    public override void Init(MonoBehaviour mono, int value)
    {
        Debug.Log("Init");
        var manager = BattleHandler.Instance;
        manager.OnLose -= manager.Restart;
        manager.OnLose += Reverse;
        manager.OnTurn += UpdateData;
    }

    private void Reverse()
    {
        var manager = BattleHandler.Instance;
        var cells = Field.Instance.Cells;
        foreach (Cell cell in cells)
        {
            if (cell.ContainedCreature != null)
            {
                Destroy(cell.ContainedCreature.gameObject);
            }
        }
        foreach(GameObject creature in _creatures)
        {
            var creatureComponent = creature.GetComponent<Creature>();
            creatureComponent.SetCell(creatureComponent.CurrentCell);
            creature.SetActive(true);
        }

        manager.OnLose -= Reverse;
        manager.OnTurn -= UpdateData;
        Destroy(this);
    }

    private void UpdateData()
    {
        foreach (GameObject creature in _creatures)
        {
            Destroy(creature);
        }
        _creatures.Clear();
        var cells = Field.Instance.Cells;
        foreach (Cell cell in cells)
        {
            if (cell.ContainedCreature != null)
            {
                var creature = Instantiate(_prefab);
                Creature creatureComponent;
                if (cell.ContainedCreature.GetType() == typeof(Hero))
                {
                    creatureComponent = creature.AddComponent<Hero>();
                }
                else
                {
                    creatureComponent = creature.AddComponent<Monster>();
                }
                creatureComponent.Init();
                creature.gameObject.SetActive(false);
                _creatures.Add(creature.gameObject);
            }
        }
    }
}
