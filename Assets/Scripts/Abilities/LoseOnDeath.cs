using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/LoseOnDeath")]
public class LoseOnDeath : Ability
{
    public override void Init(MonoBehaviour mono)
    {
        mono.GetComponent<Creature>().Health.OnDeath += Action;
    }

    public override void Action()
    {
        BattleState.Instance.HandleLose();
    }
}
