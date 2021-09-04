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

    [SerializeField]
    private Health _health;

    public Health Health => _health;

    private GameObject _projectile;

    public GameObject Projectile => _projectile;

    protected Transform _transform;

    public Transform Transform => _transform;

    public delegate void Blank();
    public event Blank OnTurn;

    private bool _ableToMove = true;

    public bool AbleToMove => _ableToMove;

    private void Awake()
    {
        _transform = transform;
    }

    private void Init()
    {
        gameObject.name = _data.name;
        _data = Instantiate(_data);

        GetComponent<MeshRenderer>().materials = _data.Materials;
        GetComponent<MeshFilter>().sharedMesh = _data.Mesh;
        
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
        cell.SetContainedCreature(this);
    }
    
    public float CompleteTurn()
    {
        var time = 0f;
        print(name + " started its turn");
        if (_ableToMove)
        {
            OnTurn?.Invoke();
            time = 1f;
        }
        print(name + " completed turn");
        return time;
    }

    public void SetAbilityToMove(bool isAble)
    {
        _ableToMove = isAble;
    }
}
