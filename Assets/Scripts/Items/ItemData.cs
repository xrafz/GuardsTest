using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField]
    private int _cost;
    public int Cost => _cost;

    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField]
    protected int[] _actionValue;
    public int[] ActionValue => _actionValue;

    [SerializeField]
    protected ItemAction[] _actions;
    public ItemAction[] Actions => _actions;

    public virtual void Use(MonoBehaviour mono)
    {
        for (int i = 0; i < _actionValue.Length; i++)
        {
            var action = Instantiate(_actions[i]);
            action.Init(mono, _actionValue[i]);
        }
    }
}
