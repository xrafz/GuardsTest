using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public delegate void Blank();
    public event Blank OnTurn;

    private void Awake()
    {
        _transform = transform;
    }

    private void Init()
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

        var renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        renderer.sharedMaterial = renderer.material;
        renderer.sharedMaterial.mainTexture = _data.Texture;

        _health = GetComponent<Health>();
        _health.Init(_data.Health);

        if (_data.Projectile != null)
        {
            _projectile = Instantiate(_data.Projectile, _transform);
            _projectile.transform.rotation = _transform.rotation;
            _projectile.SetActive(false);
        }

        var abilities = _data.Abilities;
        for (int i = 0; i < abilities.Length; i++)
        {
            var ability = abilities[i];
            ability = Instantiate(ability);
            ability.Init(this);
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
        _data = data;
        Init();
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
        /*
        _transform.DOMove(cell.transform.position, time).OnComplete(() =>
        {
            _animator.Play("Idle");
        });
        */
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
}
