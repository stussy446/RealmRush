using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private bool _isPlaceable;

    private GridManager _gridManager;
    private Vector2Int _coordinates = new Vector2Int();
    public bool IsPlaceable { get { return _isPlaceable; } }

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }
    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);

            if (!IsPlaceable)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            bool isPlaced = _towerPrefab.CreateTower(_towerPrefab, transform.position);
            _isPlaceable = !isPlaced;
        }
    }
}

