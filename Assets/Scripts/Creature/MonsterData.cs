using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Monster")]
public class MonsterData : CreatureData
{
    [Header("Monster Specific Stats")]
    [SerializeField]
    private int _movementDistance = 1;
    public int MovementDistance => _movementDistance;

    [SerializeField]
    private int _castTime = 2;
    public int CastTime => _castTime;
}
