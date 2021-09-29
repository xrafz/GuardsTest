using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField]
    private LevelHolder[] _levels;
    public LevelHolder[] Levels => _levels;

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
        GameSession.LoadSave();

        Debug();

        var availableLevels = GameSession.Save.AvailableLevels;
        var completedLevels = GameSession.Save.CompletedLevels;
        List<LevelData> levelsToOpen = new List<LevelData>();
        foreach (LevelHolder level in _levels)
        {
            if (completedLevels.Contains(level.Data.name))
            {
                foreach (LevelData levelToOpen in level.Data.LevelsToOpen)
                {
                    if (!levelsToOpen.Contains(levelToOpen))
                    {
                        levelsToOpen.Add(levelToOpen);
                    }
                }
            }
            else if (availableLevels.Contains(level.Data.name))
            {
                levelsToOpen.Add(level.Data);
            }
        }

        foreach (LevelHolder level in _levels)  //включаем коллайдеры для каждого разблокированного уровня
        {
            if (levelsToOpen.Contains(level.Data) || level.Data.InitiallyOpen)
            {
                level.SetColliderStatus(true);
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
        _shop.CalculateBudget();
    }

    private void Debug()
    {
        print($"Mithril: {GameSession.Save.Mithril}");
        print($"Stars: {GameSession.Save.Stars}");
    }
}
