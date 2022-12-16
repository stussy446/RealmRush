using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool _isPlaceable;
    [SerializeField] private GameObject _towerPrefab;
    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            Instantiate(_towerPrefab, transform.position, Quaternion.identity);
            _isPlaceable = false;
        }
    }
}

