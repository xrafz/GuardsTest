using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroUpgrade : MonoBehaviour 
{
    [SerializeField]
    private HeroUpgradeHolder[] _heroes;

    private void OnEnable()
    {
        foreach (HeroUpgradeHolder slot in _heroes)
        {
            slot.Init(GameSession.Heroes[Random.Range(0, GameSession.Heroes.Length)]);
        }
    }
}
