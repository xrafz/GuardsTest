using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{

    private void Start()
    {
        _transform.rotation = Quaternion.Euler(0, 270, 0);
    }


}
