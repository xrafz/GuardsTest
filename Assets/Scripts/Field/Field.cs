using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private int _rows;
    [SerializeField]
    private int _columns;

    [SerializeField]
    private CellGenerator _cellGenerator;
    [SerializeField]
    private CreatureGenerator _heroGenerator, _mobGenerator;

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
        _cells = _cellGenerator.Spawn(_rows, _columns);
        _heroGenerator.InitialGeneration();
        _mobGenerator.InitialGeneration();
    }
}
