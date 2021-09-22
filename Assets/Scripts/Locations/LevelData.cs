using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Locations/LevelData"), System.Serializable]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private LocationData[] _locations;

    public LocationData[] Locations => _locations;

    [SerializeField]
    private LevelData[] _levelsToOpen;

    public LevelData[] LevelsToOpen => _levelsToOpen;
}
