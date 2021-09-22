using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession
{
    public static LevelData CurrentLevel { get; private set; }

    public static LocationData CurrentLocation { get; private set; }

    public static int CurrentLocationID { get; private set; }
    
    [SerializeField]
    public static SaveData CurrentSave { get; private set; }

    public static void SetCurrentLevel(LevelData data)
    {
        CurrentLevel = data;
        CurrentLocationID = 0;
        CurrentLocation = CurrentLevel.Locations[CurrentLocationID];
    }

    public static void SetNextLocation()
    {
        CurrentLocationID++;
        CurrentLocation = CurrentLevel.Locations[CurrentLocationID];
    }

    public static void CompleteCurrentLevel()
    {
        if (CurrentSave.CompletedLevels.ContainsKey(CurrentLevel.name))
        {
            CurrentSave.CompletedLevels[CurrentLevel.name] = true;
        }
        else
        {
            CurrentSave.CompletedLevels.Add(CurrentLevel.name, true);
        }

        foreach (LevelData level in CurrentLevel.LevelsToOpen)
        {
            if (CurrentSave.AvailableLevels.ContainsKey(level.name))
            {
                CurrentSave.AvailableLevels[level.name] = true;
            }
            else
            {
                CurrentSave.AvailableLevels.Add(level.name, true);
            }
            MonoBehaviour.print(CurrentSave.AvailableLevels[level.name]);
        }
        SaveLoader.Save(CurrentSave);
    }

    public static void LoadProgressData()
    {
        CurrentSave = SaveLoader.LoadSave();
    }
}

[System.Serializable]
public class SaveData
{
    public Dictionary<string, bool> CompletedLevels { get; private set; }

    public Dictionary<string, bool> AvailableLevels { get; private set; }

    public SaveData()
    {
        CompletedLevels = new Dictionary<string, bool>();
        AvailableLevels = new Dictionary<string, bool>();
    }
}

