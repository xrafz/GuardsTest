using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    private void Start()
    {
        _transform.rotation = Quaternion.Euler(0, 0, 0);
        Health.OnDeath += Die;
    }

    private void Die()
    {
        BattleState.Instance.AddDefeatedEnemy();
        MobGenerator.Instance.CreatedCreatures.Remove(this);
        _currentCell.SetContainedCreature(null);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Health.OnDeath -= Die;
    }
}
