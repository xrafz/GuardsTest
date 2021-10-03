using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maximum;
    public int Maximum => _maximum;

    [SerializeField]
    private int _current;
    public int Current => _current; //

    public event UnityAction OnHit;
    public event UnityAction OnDeath;
    public event UnityAction OnChange;

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

    public void Change(int value, int max)
    {
        _current += value;
        _maximum += max;
        if (_current <= 0)
        {
            _current = 1;
        }
        OnChange?.Invoke();
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
