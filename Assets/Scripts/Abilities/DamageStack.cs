using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DamageStack")]
public class DamageStack : Ability
{
    [SerializeField]
    private int _attacksBeforeBuff = 2;
    [SerializeField]
    private int _buffValue = 10;

    private int _currentAttacks = 0;
    private Creature _buffTarget; 

    public override void Init(MonoBehaviour mono)
    {
        _buffTarget = (Creature)mono;
    }

    private void Buff()
    {
        _buffTarget.Data.BuffDamage(_buffValue);
        BattleHandler.Instance.OnTurn += Debuff;
        BattleHandler.Instance.OnTurn -= Buff;
    }

    private void Debuff()
    {
        _buffTarget.Data.BuffDamage(-_buffValue);
        BattleHandler.Instance.OnTurn -= Debuff;
    }

    private void HandleAttacks()
    {
        _currentAttacks++;
        if (_currentAttacks >= _attacksBeforeBuff)
        { 
            BattleHandler.Instance.OnTurn += Buff;
        }
    }

   

    public override void Sub()
    {
        _currentAttacks = 0;
        _buffTarget.OnAttack += HandleAttacks;
    }

    public override void Unsub()
    {
        _buffTarget.OnAttack -= HandleAttacks;
    }
}
