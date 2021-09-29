using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Transform _playerItems;

    private int _budget;
    private int _currentCost;

    public static Shop Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CalculateBudget()
    {
        var budget = Constants.InitialBudget;
        foreach (string s in GameSession.Save.CompletedLevels)
        {
            print(s);
        }
        foreach (LevelHolder level in MapHandler.Instance.Levels)
        {
            print(level.Data.name);
            if (GameSession.Save.CompletedLevels.Contains(level.Data.name))
            {
                budget += level.Data.BudgetReward;
            }
        }
        _budget = budget;

        LimitBudget();
        Refresh();
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
        print(_budget);
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
            newItem.Set(item);
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
