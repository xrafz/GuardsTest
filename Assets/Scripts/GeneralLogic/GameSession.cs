using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession
{
    public static LevelData CurrentLevel { get; private set; }

    public static LocationData CurrentLocation { get; private set; }

    public static int CurrentLocationID { get; private set; }

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
}
