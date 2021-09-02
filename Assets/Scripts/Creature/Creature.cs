using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField]
    protected CreatureData _data;

    [SerializeField]
    protected Cell _currentCell;

    public Cell CurrentCell => _currentCell;

    [SerializeField]
    private MeshFilter _meshFilter;
    [SerializeField]
    private MeshRenderer _renderer;

    [SerializeField]
    private Health _health;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Init()
    {
        gameObject.name = _data.name;
        _data = Instantiate(_data);

        _renderer.materials = _data.Materials;
        _meshFilter.sharedMesh = _data.Mesh;

        _health.Init(_data.Health);

        var abilites = _data.Abilities;
        foreach (Ability ability in abilites)
        {
            ability.Init(this);
        }
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

    public Type GetDataType()
    {
        return _data.GetType();
    }
}
