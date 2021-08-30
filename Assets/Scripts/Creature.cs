using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField]
    private CreatureData _data;

    private void Awake()
    {
        _data = Instantiate(_data);
    }
}
