using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    private bool _usingSpecialAbility = false;

    public bool UsingSpecialAbility => _usingSpecialAbility;

    private void Start()
    {
        _transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void SetSpecialAbilityStatus(bool status)
    {
        _usingSpecialAbility = status;
    }
}
