using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Field : MonoBehaviour
{
    [SerializeField]
    private int _rows;
    [SerializeField]
    private int _columns;

    [SerializeField]
    private CellGenerator _cellGenerator;
    [SerializeField]
    private HeroGenerator _heroGenerator;
    [SerializeField]
    private MobGenerator  _mobGenerator;

    [SerializeField]
    private Cell[,] _cells;

    public Cell[,] Cells => _cells;

    public static Field Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Init();

        print(GameSession.Location);
    }

    private void Init()
    {
        var field = Instantiate(GameSession.Location.Environment);
        _cells = _cellGenerator.Spawn(_rows, _columns);
        InitSpawners();
        BattleState.Instance.SetCells(_cells);
    }

    public void InitSpawners()
    {
        _heroGenerator.SetCreatures(GameSession.Heroes);
        _heroGenerator.InitialGeneration();
        _mobGenerator.SetCreatures(GameSession.Location.Mobs);
        _mobGenerator.InitialGeneration();
    }
}
