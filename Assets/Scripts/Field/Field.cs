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
    }

    private void Init()
    {
        _cells = _cellGenerator.Spawn(_rows, _columns);
        InitSpawners();
        GameplayState.Instance.SetCells(_cells);
    }

    public void InitSpawners()
    {
        _heroGenerator.InitialGeneration();
        _mobGenerator.InitialGeneration();
    }
}
