using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _itemSlots = new List<ItemData>(); //предметы для спавна // todo: брать с сейва

    [SerializeField]
    private List<ItemData> _selectedItems = new List<ItemData>();

    [SerializeField]
    private TMP_Text _budgetText;

    [SerializeField]
    private ItemHolder _itemPrefab;
    [SerializeField]
    private Transform _playerItems, _shopItems;

    private int _budget;
    private int _currentCost;
    private List<string> _unlockedItems = new List<string>();

    public static Shop Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void LoadItems()
    {
        List<string> itemsToLoad = new List<string>();
        foreach (string item in Constants.InitialItems)
        {
            itemsToLoad.Add(item);
        }
        foreach (string item in _unlockedItems)
        {
            itemsToLoad.Add(item);
        }

        foreach (string item in itemsToLoad)
        {
            Addressables.LoadAssetAsync<ItemData>(item).Completed += handle =>
            {
                var newItem = Instantiate(_itemPrefab, _shopItems);
                newItem.Set(handle.Result, ItemHolder.Types.Buying);
                newItem.name = newItem.Item.name;
                _itemSlots.Add(handle.Result);
            };
        }
    }

    public void Init()
    {
        gameObject.SetActive(true);
        GameSession.Items.Clear();
        GameSession.SetGold(0);
        HandleLevelRewards();
        LoadItems();
        LimitBudget();
        Refresh();
    }

    private void HandleLevelRewards()
    {
        var budget = Constants.InitialBudget;
        var levels = MapHandler.Instance.Levels;
        foreach (LevelHolder level in levels)
        {
            if (GameSession.Save.CompletedLevels.Contains(level.Data.name))
            {
                budget += level.Data.BudgetReward;
                foreach (string item in level.Data.ItemsReward)
                {
                    _unlockedItems.Add(item);
                }
            }
        }
        _budget = budget;
    }

    private void LimitBudget()
    {
        var levelBudget = GameSession.Level.BudgetLimit;
        if (levelBudget != 0 && levelBudget < _budget)
        {
            _budget = levelBudget;
        }
    }

    public void Refresh()
    {
        _currentCost = 0;
        foreach (ItemData item in _selectedItems)
        {
            _currentCost += item.Cost;
        }
        _budgetText.text = $"{_currentCost}/{_budget}";
    }

    public void Add(ItemData item)
    {
        print(_budget);
        if (_currentCost + item.Cost <= _budget)
        {
            _selectedItems.Add(item);
            var newItem = Instantiate(_itemPrefab, _playerItems);
            newItem.Set(item, ItemHolder.Types.Selling);
            Refresh();
        }
    }

    public void Remove(ItemData item)
    {
        _selectedItems.Remove(item);
        Refresh();
    }

    public void LoadBattle()
    {
        GameSession.SetItems(_selectedItems);
        SceneManager.LoadScene(1);
    }
}
