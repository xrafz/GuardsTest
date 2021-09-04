using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maximum;

    public int Maximum => _maximum;

    [SerializeField]
    private int _current;

    public int Current => _current; //

    public delegate void Blank();
    public event Blank OnHit;
    public event Blank OnDeath;
    public event Blank OnChange;

    public void Init(int value)
    {
        _maximum = value;
        _current = value;
    }

    public void Change(int value)
    {
        _current += value;
        OnChange?.Invoke();
        if (value < 0)
        {
            OnHit?.Invoke();
        }
        Check();
    }

    private void Check()
    {
        if (_current < 1)
        {
            OnDeath?.Invoke();
        }
        else if (_current > _maximum)
        {
            _current = _maximum;
        }
    }
}
