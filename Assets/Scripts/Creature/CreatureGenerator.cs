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
        CheckQuantity();
        for (int i = 0; i < _defaultQuantity; i++ )
        {
            var creature = Instantiate(_prefab, _transform);
            var creatureComponent = creature.AddComponent<T>();
            //creatureComponent.SetData(_creatures[i]);
            _createdCreatures.Add(creatureComponent);
        }
        ChangePositions();
        SetData();
    }

    protected void CheckQuantity()
    {
        if (_defaultQuantity > _creatures.Length || _defaultQuantity == 0)
        {
            _defaultQuantity = _creatures.Length;
        }
    }

    protected abstract void ChangePositions();

    protected abstract void SetData();
}
