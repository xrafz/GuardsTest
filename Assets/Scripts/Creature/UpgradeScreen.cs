using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScreen : MonoBehaviour 
{
    [SerializeField]
    private HeroUpgradeHolder[] _heroes;

    [SerializeField]
    private TMP_Text _gold;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        foreach (HeroUpgradeHolder slot in _heroes)
        {
            slot.Init(GameSession.Heroes[Random.Range(0, GameSession.Heroes.Length)]);
        }

        BattleState.Instance.OnWin += () =>
        {
            gameObject.SetActive(true);
        };

        GameSession.OnGoldChange += RefreshGold;

        gameObject.SetActive(false);
    }

    private void RefreshGold()
    {
        _gold.text = GameSession.Gold.ToString();
    }

    private void OnDestroy()
    {
        GameSession.OnGoldChange -= RefreshGold;
    }
}
