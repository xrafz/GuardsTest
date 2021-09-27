using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    protected override void Awake()
    {
        base.Awake();
        _transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
