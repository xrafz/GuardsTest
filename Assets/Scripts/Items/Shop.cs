using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _itemSlots = new List<ItemData>(); //предметы для спавна // todo: брать с сейва

    [SerializeField]
    public List<ItemData> SelectedItems { get; private set; } = new List<ItemData>();

    [SerializeField]
    private TMP_Text _budgetText;

    private int _budget;
    private int _currentCost;

    public static Shop Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetBudget()
    {
        _budget = GameSession.Save.Budget;

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
        foreach (ItemData item in SelectedItems)
        {
            _currentCost += item.Cost;
        }
        _budgetText.text = _currentCost.ToString();
    }

    public void Add(ItemData item)
    {
        print(_budget);
        if (_currentCost + item.Cost <= _budget)
        {
            SelectedItems.Add(item);
            Refresh();
        }
    }

    public void Remove(ItemData item)
    {
        SelectedItems.Remove(item);
        Refresh();
    }
}
