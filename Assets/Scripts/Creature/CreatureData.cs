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
    [SerializeField]
    private Mesh _mesh;

    public Mesh Mesh => _mesh;

    [SerializeField]
    private Material[] _materials;

    public Material[] Materials => _materials;
    #endregion

    [SerializeField]
    private GameObject _projectile;

    public GameObject Projectile => _projectile;

    public void AddDamage(int value)
    {
        _damage += value;
    }
}
