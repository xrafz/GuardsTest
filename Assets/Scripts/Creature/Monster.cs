using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    protected override void Awake()
    {
        base.Awake();
        _transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Start()
    {
        Health.OnDeath += Die;
    }

    private void Die()
    {
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        SetAbilityToMove(false);
        Animator.Play("Death");
        BattleHandler.Instance.AddDefeatedEnemy();
        MobGenerator.Instance.CreatedCreatures.Remove(this);
        yield return new WaitForSeconds(0.8f);
        _currentCell.SetContainedCreature(null);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Health.OnDeath -= Die;
    }
}
