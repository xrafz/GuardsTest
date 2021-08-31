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
    private Cell[,] _cells;
    private void Awake()
    {
        _cells = _cellGenerator.Spawn(_rows, _columns);
    }
}
