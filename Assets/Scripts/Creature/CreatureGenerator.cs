using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureGenerator : MonoBehaviour
{
    [SerializeField]
    protected CreatureData[] _creatures;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private int _defaultQuantity = 4;
    [SerializeField]
    private Quaternion _creaturesRotation;

    [SerializeField]
    protected List<Creature> _createdCreatures = new List<Creature>();

    public List<Creature> CreatedCreatures => _createdCreatures;

    private Transform _transform;
    protected Field _field;

    private void Awake()
    {
        _transform = transform;
    }

    public abstract void InitialGeneration();

    public virtual void InitialGeneration<T>() where T : Creature
    {
        CheckQuantity();
        for (int i = 0; i < _defaultQuantity; i++ )
        {
            var creature = Instantiate(_prefab, _transform);
            var creatureComponent = creature.AddComponent<T>();
            creature.transform.rotation = _creaturesRotation;
            creatureComponent.SetData(_creatures[i]);
            _createdCreatures.Add(creatureComponent);
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
