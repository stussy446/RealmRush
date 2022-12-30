using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;

    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        CreateGrid();
    }

    /// <summary>
    /// Starts at 0,0 and loops through entire grid, creating a new node for each node
    /// </summary>
    private void CreateGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                _grid.Add(coordinates, new Node(coordinates, true));
                Debug.Log($"{_grid[coordinates].coordinates} = {_grid[coordinates].isWalkable}");
            }
        }
    }
}
