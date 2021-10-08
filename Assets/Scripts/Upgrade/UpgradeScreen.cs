using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScreen : MonoBehaviour 
{
    [SerializeField]
    private HeroUpgradeHolder[] _heroSlots;
    public HeroUpgradeHolder[] HeroSlots => _heroSlots;

    [SerializeField]
    private UpgradeScreenItems _itemsBar;

    [SerializeField]
    private TMP_Text _gold;

    public static UpgradeScreen Instance;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Instance = this;

        print(_heroSlots);

        foreach (HeroUpgradeHolder slot in _heroSlots)
        {
            slot.Init(GameSession.Heroes[Random.Range(0, GameSession.Heroes.Length)]);
        }
        BattleHandler.Instance.OnWin += () =>
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

    private void OnEnable()
    {
        BattleHandler.Instance.SetInteractivityStatus(false);
    }

    public void NextLevel()
    {
        BattleHandler.Instance.Restart();
    }

    private void OnDestroy()
    {
        GameSession.OnGoldChange -= RefreshGold;
    }
}
