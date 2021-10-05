using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Locations/LocationData")]
public class LocationData : ScriptableObject
{
    [SerializeField]
    private MonsterData[] _mobs;
    public MonsterData[] Mobs => _mobs;

    [SerializeField]
    private GameObject _environment;
    public GameObject Environment => _environment;

    [SerializeField]
    private int _enemiesToKill;
    public int EnemiesToKill => _enemiesToKill;

    [SerializeField]
    private int _goldReward;
    public int GoldReward => _goldReward;

    [SerializeField]
    private int _rewardRandomness = 0;
    public int RewardRandomness => _rewardRandomness;
}
