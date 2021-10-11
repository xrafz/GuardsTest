using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/HealOnKill")]
public class HealOnKill : Ability
{
    [SerializeField]
    private int _healValue;

    private Creature _creature;
    public override void Init(MonoBehaviour mono)
    {
        _creature = (Creature)mono;
    }

    private void Action()
    {
        _creature.Health.Change(_healValue);
    }

    public override void Sub()
    {
        _creature.OnEnemyKilled += Action;
    }

    public override void Unsub()
    {
        _creature.OnEnemyKilled -= Action;
    }
}
