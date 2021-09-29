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

    [SerializeField]
    private bool _initiallyOpen;
    public bool InitiallyOpen => _initiallyOpen;

    [SerializeField]
    private int _budgetLimit;
    public int BudgetLimit => _budgetLimit;

    [Header("������� �� �������")]
    [SerializeField]
    private int _mithrilReward;
    public int MithrilReward => _mithrilReward;

    [SerializeField]
    private int _budgetReward;
    public int BudgetReward => _budgetReward;

    [SerializeField]
    private int _starsReward;
    public int StarsReward => _starsReward;
}
