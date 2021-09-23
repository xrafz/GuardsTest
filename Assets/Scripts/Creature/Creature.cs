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

    public Health Health { get; private set; }

    public GameObject Body { get; private set; }

    public GameObject Projectile { get; private set; }

    protected Transform _transform;

    public Transform Transform => _transform;

    public Animator Animator { get; private set; }

    public bool AbleToMove { get; private set; } = true;

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
        Body = Instantiate(_data.Body, _transform);
        Animator = Body.GetComponent<Animator>();
        if (Animator == null)
        {
            Animator = Body.AddComponent<Animator>();
        }
        Animator.runtimeAnimatorController = _data.Animator;

        var renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        renderer.sharedMaterial = renderer.material;
        renderer.sharedMaterial.mainTexture = _data.Texture;

        /*
        GetComponent<MeshRenderer>().materials = _data.Materials;
        GetComponent<MeshFilter>().sharedMesh = _data.Mesh;
        */




        Health = GetComponent<Health>();
        Health.Init(_data.Health);

        if (_data.Projectile != null)
        {
            Projectile = Instantiate(_data.Projectile, _transform);
            Projectile.transform.rotation = _transform.rotation;
            Projectile.SetActive(false);
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
        if (AbleToMove)
        {
            OnTurn?.Invoke();
            time = 1f;
        }
        print(name + " completed turn");
        return time;
    }

    public void SetAbilityToMove(bool isAble)
    {
        AbleToMove = isAble;
    }
}
