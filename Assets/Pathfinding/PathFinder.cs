using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Node _currentSearchNode;

    private Vector2Int[] _directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    private GridManager _gridManager;
    private Dictionary<Vector2Int, Node> _grid;

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();

        if (_gridManager != null)
        {
            _grid = _gridManager.Grid;
        }
    }

    private void Start()
    {
        ExploreNeighbors();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighborCoordinates = _currentSearchNode.coordinates + direction;

            if (_grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(_grid[neighborCoordinates]);

                //TODO: Remove after testing 
                _grid[neighborCoordinates].isExplored = true;
                _grid[_currentSearchNode.coordinates].isPath = true;
            }
        }
    }
}
