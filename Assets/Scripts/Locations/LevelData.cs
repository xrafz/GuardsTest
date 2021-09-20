using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Locations/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private LocationData[] _locations;

    public LocationData[] Locations => _locations;
}
