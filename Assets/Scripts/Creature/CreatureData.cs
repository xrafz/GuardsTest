using System;
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

    [Tooltip("Slashing, Piercing, Blunt, DarkMagic, ElementalMagic, AstralMagic")]
    [SerializeField]
    private int _damageType;
    public int DamageType => _damageType;

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

    [Tooltip("Slashing, Piercing, Blunt, DarkMagic, ElementalMagic, AstralMagic")]
    [SerializeField]
    private int[] _resistance = 
    {
        0,
        0,
        0,
        0,
        0,
        0
    };
    public int[] Resistance => _resistance;

    #region Appearance
    [SerializeField, Header("Appearance")]
    private GameObject _body;
    public GameObject Body => _body;

    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

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

    public void ChangeHealth(int value)
    {
        _health += value;
    }

    public void ChangeAbilityPower(int value)
    {
        _abilityPower += value;
    }

    public void SetDamage(int value)
    {
        _damage = value;
    }

    public void SetDamageType(int value)
    {
        _damageType = value;
    }

    private void OnValidate()
    {
        var maxLength = Enum.GetNames(typeof(Constants.DamageTypes)).Length - 1;
        _damageType = Mathf.Clamp(_damageType, 0, maxLength);
    }
}
