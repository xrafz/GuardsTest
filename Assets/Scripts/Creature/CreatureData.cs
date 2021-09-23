using UnityEngine;

public abstract class CreatureData : ScriptableObject
{
    #region Stats
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
    private Ability[] _abilities;
    public Ability[] Abilities  => _abilities;

    #region Appearance
    [SerializeField, Header("Appearance")]
    private GameObject _body;
    public GameObject Body => _body;

    [SerializeField]
    private Texture _texture;
    public Texture Texture => _texture;

    [SerializeField]
    private AnimatorOverrideController _animator;
    public AnimatorOverrideController Animator => _animator;
    #endregion

    [SerializeField]
    private GameObject _projectile;
    public GameObject Projectile => _projectile;

    public void ChangeDamage(int value)
    {
        _damage += value;
    }
}
