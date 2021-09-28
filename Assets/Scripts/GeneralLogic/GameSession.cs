using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession
{
    public static LevelData Level { get; private set; }

    public static LocationData Location { get; private set; }

    public static int LocationID { get; private set; }

    //public Dictionary<ItemData, int> Items { get; private set; } = new Dictionary<ItemData, int>(); //   item | quantity
    public List<ItemData> Items { get; private set; } = new List<ItemData>();

    [SerializeField]
    public static SaveData Save { get; private set; }

    public static void SetCurrentLevel(LevelData data)
    {
        Level = data;
        LocationID = 0;
        Location = Level.Locations[LocationID];
    }

    public static void SetNextLocation()
    {
        LocationID++;
        Location = Level.Locations[LocationID];
    }

    public static void CompleteCurrentLevel()
    {
        if (Save.CompletedLevels.ContainsKey(Level.name))
        {
            Save.CompletedLevels[Level.name] = true;
        }
        else
        {
            Save.CompletedLevels.Add(Level.name, true);

            Save.ChangeBudget(Level.BudgetReward); // один раз прошел - один раз получил награды
            Save.ChangeMitrhril(Level.MithrilReward);
            Save.ChangeStars(Level.StarsReward);
        }

        foreach (LevelData level in Level.LevelsToOpen)
        {
            if (Save.AvailableLevels.ContainsKey(level.name))
            {
                Save.AvailableLevels[level.name] = true;
            }
            else
            {
                Save.AvailableLevels.Add(level.name, true);
            }
            MonoBehaviour.print(Save.AvailableLevels[level.name]);
        }

        SaveLoader.Save(Save);
    }

    public static void LoadProgressData()
    {
        Save = SaveLoader.LoadSave();
    }
}

[System.Serializable]
public class SaveData
{
    public Dictionary<string, bool> CompletedLevels { get; private set; }

    public Dictionary<string, bool> AvailableLevels { get; private set; }

    public int Mithril { get; private set; } = 0; //изначальное значение

    public int Budget { get; private set; } = 250; //изначальное значение

    public int Stars { get; private set; } = 0;//изначальное значение

    public void ChangeMitrhril(int quantity)
    {
        Mithril += quantity;
    }

    public void ChangeBudget(int quantity)
    {
        Budget += quantity;
    }

    public void ChangeStars(int quantity)
    {
        Stars += quantity;
    }

    public SaveData()
    {
        CompletedLevels = new Dictionary<string, bool>();
        AvailableLevels = new Dictionary<string, bool>();
    }
}