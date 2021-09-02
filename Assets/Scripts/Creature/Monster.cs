using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    private bool _castingAbility = false;

    public bool CastingAbility => _castingAbility;

    private void Start()
    {
        _transform.rotation = Quaternion.Euler(0, 270, 0);
    }

    public void SetCastingStatus(bool status)
    {
        _castingAbility = status;
    }
}
