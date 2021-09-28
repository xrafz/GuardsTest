using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField]
    private LevelHolder[] _levels;

    [SerializeField]
    private Shop _shop;

    public static MapHandler Instance;

    private void Awake()
    {
        Init();
        Instance = this;
    }

    private void Init()
    {
        GameSession.LoadProgressData();

        Debug();

        foreach (LevelHolder level in _levels)  //включаем коллайдеры для каждого разблокированного уровня
        {
            if (GameSession.Save.AvailableLevels.ContainsKey(level.Data.name))
            {
                level.SetColliderStatus(GameSession.Save.AvailableLevels[level.Data.name]);
            }
            else
            {
                level.SetColliderStatus(false);
            }
        }
    }

    public void LoadShop()
    {
        gameObject.SetActive(false);
        _shop.gameObject.SetActive(true);
        _shop.SetBudget();
    }

    private void Debug()
    {
        print($"Mithril: {GameSession.Save.Mithril}");
        print($"Budget: {GameSession.Save.Budget}");
        print($"Stars: {GameSession.Save.Stars}");
    }
}
