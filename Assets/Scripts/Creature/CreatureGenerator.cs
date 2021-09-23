using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureGenerator<T> : MonoBehaviour where T : Creature
{
    [SerializeField]
    protected CreatureData[] _creatures;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private int _defaultQuantity = 4;

    [SerializeField]
    protected List<T> _createdCreatures = new List<T>();

    public List<T> CreatedCreatures => _createdCreatures;

    private Transform _transform;
    protected Field _field;

    private void Awake()
    {
        _transform = transform;
    }

    public virtual void InitialGeneration()
    {
        /*
        foreach(T creature in _createdCreatures)
        {
            Destroy(creature.gameObject);
        }
        */
        for (int i = _createdCreatures.Count - 1; i >= 0; i--)
        {
            Destroy(_createdCreatures[i].gameObject);
        }

        _createdCreatures.Clear();
        CheckQuantity();
        for (int i = 0; i < _defaultQuantity; i++ )
        {
            GenerateCreature();
        }
        ChangePositions();
    }

    public Creature GenerateCreature()
    {
        var creature = Instantiate(_prefab, _transform);
        var creatureComponent = creature.AddComponent<T>();
        _createdCreatures.Add(creatureComponent);
        SetData(creatureComponent);
        return creatureComponent;
    }

    protected void CheckQuantity()
    {
        if (_defaultQuantity == 0)
        {
            _defaultQuantity = _creatures.Length;
        }
    }

    public void SetCreatures(CreatureData[] creatures)
    {
        _creatures = creatures;
    }

    protected abstract void ChangePositions();

    protected abstract void SetData(Creature creature);
}
