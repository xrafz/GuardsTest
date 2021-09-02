using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    public event Blank OnSpecialAbility;
    
    private void Start()
    {
        _transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void UseSpecialAbility()
    {
        OnSpecialAbility?.Invoke();
    }
}
