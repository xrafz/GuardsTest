using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField]
    private LevelHolder[] _levels;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        GameSession.LoadProgressData();

        foreach (LevelHolder level in _levels)  //включаем коллайдеры для каждого разблокированного уровня
        {
            if (GameSession.CurrentSave.AvailableLevels.ContainsKey(level.Data.name))
            {
                level.SetColliderStatus(GameSession.CurrentSave.AvailableLevels[level.Data.name]);
            }
            else
            {
                level.SetColliderStatus(false);
            }
        }
    }
}
