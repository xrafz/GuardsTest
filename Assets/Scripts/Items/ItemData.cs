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

    public abstract void Use(MonoBehaviour mono);
}
