using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureGenerator : MonoBehaviour
{
    [SerializeField]
    protected CreatureData[] _creatures;
    [SerializeField]
    private Creature _prefab;
    [SerializeField]
    private int _defaultQuantity = 4;

    [SerializeField]
    protected List<Creature> _createdCreatures = new List<Creature>();

    public List<Creature> CreatedCreatures => _createdCreatures;

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
            creature.SetData(_creatures[i]);
            _createdCreatures.Add(creature);
        }
        ChangePositions();
    }

    protected void CheckQuantity()
    {
        if (_defaultQuantity > _creatures.Length || _defaultQuantity == 0)
        {
            _defaultQuantity = _creatures.Length;
        }
    }

    protected abstract void ChangePositions();
}
