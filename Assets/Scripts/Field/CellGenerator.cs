using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    [SerializeField]
    private Cell _cellPrefab;
    [SerializeField]
    private Vector3 _position = Vector3.zero;
    [SerializeField]
    private bool _isBlack = true;

    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
    }
    public Cell[,] Spawn(int rows, int columns)
    {
        Cell[,] cells = new Cell[rows, columns];
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            _position.x = 0;
            for (int currentColumn = 0; currentColumn < columns; currentColumn++)
            {
                var cell = Instantiate(_cellPrefab, _position, Quaternion.Euler(90, 0, 0));
                cell.gameObject.name = string.Format("{0}, {1}", currentRow, currentColumn);
                cell.transform.parent = _transform;
                cell.SetRendererStatus(_isBlack);
                _isBlack = !_isBlack;
                _position.x += 2;
                cells[currentRow, currentColumn] = cell;
            }
            _position.z += 2;
        }
        return cells;
    }
}
