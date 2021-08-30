using UnityEngine;

public abstract class CreatureData : ScriptableObject
{
    [SerializeField]
    private int _health;

    public int Health => _health;

    [SerializeField]
    private int _damage;

    public int Damage => _damage;

    [SerializeField]
    private int _attackRange;

    public int AttackRange => _attackRange;

    [SerializeField]
    private Ability _abilities;

    public Ability Abilities  => _abilities;
}
