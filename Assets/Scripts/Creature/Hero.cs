using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    private void Start()
    {
        _transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
