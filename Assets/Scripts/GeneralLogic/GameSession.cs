using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSession
{
    public static LevelData Level { get; private set; }

    public static LocationData Location { get; private set; }

    public static int LocationID { get; private set; }

    public static List<ItemData> Items { get; private set; } = new List<ItemData>();

    public static int Gold { get; private set; }

    public static HeroData[] Heroes { get; private set; }

    public static SaveData Save { get; private set; }

    public static UnityAction OnGoldChange;

    public static void CompleteCurrentLevel()
    {
        Debug.Log($"{Level.name}");
        if (!Save.CompletedLevels.Contains(Level.name))
        {
            Debug.Log("add");
            Save.CompletedLevels.Add(Level.name);

            Save.ChangeMitrhril(Level.MithrilReward);
            Save.ChangeStars(Level.StarsReward);
        }

        foreach (LevelData level in Level.LevelsToOpen)
        {
            if (!Save.AvailableLevels.Contains(level.name))
            {
                Save.AvailableLevels.Add(level.name);
            }
        }

        SaveLoader.Save(Save);
    }

    public static void LoadSave()
    {
        Save = SaveLoader.LoadSave();
    }

    public static void SetCurrentLevel(LevelData data)
    {
        Level = data;
        LocationID = 0;
        Location = Level.Locations[LocationID];
        Gold = 0;
    }

    public static void SetNextLocation()
    {
        LocationID++;
        Location = Level.Locations[LocationID];
    }

    public static void SetItems(List<ItemData> items)
    {
        Items = items;
    }

    public static void SetGold(int value)
    {
        Gold = value;
    }

    public static void ChangeGold(int value)
    {
        Gold += value;
        OnGoldChange?.Invoke();
    }

    public static void ChangeGold(int value, int randomness)
    {
        value = Mathf.Clamp(value, value, value + Random.Range(-randomness, randomness));
        ChangeGold(value);
    }

    public static void SetHeroes(List<HeroData> heroes)
    {
        Heroes = heroes.ToArray();
    }
}

[System.Serializable]
public class SaveData
{
    public List<string> CompletedLevels { get; private set; } = new List<string>();

    public List<string> AvailableLevels { get; private set; } = new List<string>();

    public int Mithril { get; private set; }

    public int Stars { get; private set; }

    public void ChangeMitrhril(int quantity)
    {
        Mithril += quantity;
    }

    public void ChangeStars(int quantity)
    {
        Stars += quantity;
    }

    public SaveData()
    {
        CompletedLevels = new List<string>();
        AvailableLevels = new List<string>();
        Mithril = Constants.InitialMithril;
        Stars = Constants.InitialStars;
    }
}