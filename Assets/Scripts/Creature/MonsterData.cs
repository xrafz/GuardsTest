using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Monster")]
public class MonsterData : CreatureData
{
    [SerializeField] //TODO ������� � MonsterData
    private int _movementDistance = 1;
    public int MovementDistance => _movementDistance;
}
