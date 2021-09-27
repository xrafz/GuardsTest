using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureData : ScriptableObject
{
    #region Stats
    [Header("Stats")]
    [SerializeField]
    private int _health;
    public int Health => _health;

    [SerializeField]
    private int _damage;
    public int Damage => _damage;

    [SerializeField]
    private int _attackRange;
    public int AttackRange => _attackRange;
    #endregion

    [SerializeField]
    private int _abilityPower;
    public int AbilityPower => _abilityPower;

    [SerializeField]
    private Ability[] _abilities;
    public Ability[] Abilities  => _abilities;

    #region Appearance
    [SerializeField, Header("Appearance")]
    private GameObject _body;
    public GameObject Body => _body;

    [SerializeField]
    private AnimatorOverrideController _animator;
    public AnimatorOverrideController Animator => _animator;

    [SerializeField]
    private Vector3 _healthBarOffset;
    public Vector3 HealthBarOffset => _healthBarOffset;
    #endregion

    [SerializeField]
    private GameObject _projectile;
    public GameObject Projectile => _projectile;

    public void ChangeDamage(int value)
    {
        _damage += value;
    }
}
