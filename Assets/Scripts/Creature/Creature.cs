using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Creature : MonoBehaviour
{
    [SerializeField]
    protected CreatureData _data;
    public CreatureData Data => _data;

    [SerializeField]
    protected Cell _currentCell;
    public Cell CurrentCell => _currentCell;

    private Health _health;
    public Health Health => _health;

    private GameObject _body;
    public GameObject Body => _body;

    private GameObject _projectile;
    public GameObject Projectile => _projectile;

    protected Transform _transform;
    public Transform Transform => _transform;

    private Animator _animator;
    public Animator Animator => _animator;

    private bool _ableToMove = true;
    public bool AbleToMove => _ableToMove;

    private bool _castingAbility = false;
    public bool CastingAbility => _castingAbility;

    private float _turnTime;

    protected Ability[] _abilities;

    public event UnityAction OnTurn;
    public event UnityAction<Creature> OnHit;
    public event UnityAction OnEnemyKilled;
    public event UnityAction OnAttack;

    protected virtual void Awake()
    {
        _transform = transform;
    }

    public virtual void Init()
    {
        gameObject.name = _data.name;
        _data = Instantiate(_data);
        _body = Instantiate(_data.Body, _transform);
        _animator = _body.GetComponent<Animator>();
        if (_animator == null)
        {
            _animator = _body.AddComponent<Animator>();
        }
        _animator.runtimeAnimatorController = _data.Animator;

        _health = GetComponent<Health>();
        _health.Init(_data.Health);

        if (_data.Projectile != null)
        {
            _projectile = Instantiate(_data.Projectile, _transform);
            _projectile.transform.rotation = _transform.rotation;
            _projectile.SetActive(false);
        }

        _abilities = _data.Abilities;
        for (int i = 0; i < _abilities.Length; i++)
        {
            _abilities[i] = Instantiate(_abilities[i]);
            _abilities[i].Init(this);
            _abilities[i].Sub();
        }

        SpawnAnimation();
    }

    private void SpawnAnimation()
    {
        _transform.DOScale(0, 0).OnComplete(() =>
        {
            _transform.DOScale(1f, 0.5f);
        });
    }

    public void SetData(CreatureData data)
    {
        if (_data)
        {
            Destroy(_data);
        }
        _data = data;
    }

    public void SetCell(Cell cell)
    {
        _currentCell = cell;
        _transform.position = cell.transform.position;
        cell.SetContainedCreature(this);
    }

    public void SetCell(Cell cell, float time)
    {
        _currentCell = cell;
        _transform.DOMove(cell.transform.position, time);
        cell.SetContainedCreature(this);
    }
    
    public float CompleteTurn()
    {
        _turnTime = 0f;
        print(name + " started its turn");
        if (_ableToMove)
        {
            _turnTime = 0.5f;
            OnTurn?.Invoke();
        }
        print(_turnTime);
        print(name + " completed turn");
        return _turnTime;
    }

    public void SetAbilityToMove(bool isAble)
    {
        _ableToMove = isAble;
    }

    public void SetTurnTime(float time)
    {
        _turnTime = time;
    }

    public void SetCastingStatus(bool status)
    {
        _castingAbility = status;
    }

    public void Play(string state)
    {
        _animator.Play(state);
    }

    public void Attack()
    {
        OnAttack?.Invoke();
    }

    public void Hit(Creature attacker)
    {
        OnHit?.Invoke(attacker);
    }

    public void EnemyKilled()
    {
        OnEnemyKilled?.Invoke();
    }

    private void OnDestroy()
    {
        foreach (Ability ability in _abilities)
        {
            ability.Destroy();
        }
    }
}
