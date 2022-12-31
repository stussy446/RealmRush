using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _blockedColor = Color.gray;
    [SerializeField] private Color _exploredColor = Color.yellow;
    [SerializeField] private Color _pathColor = Color.red;

    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();
    private bool _showingLabels;
    private GridManager _gridManager;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _label.enabled = _showingLabels;
        _gridManager = FindObjectOfType<GridManager>();

        DisplayCoordinates();
        UpdateObjectName();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            _label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void SetLabelColor()
    {
        if (_gridManager == null) { return; }

        Node node = _gridManager.GetNode(_coordinates);
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            _label.color = _blockedColor;
        }
        else if (node.isPath)
        {
            _label.color = _pathColor;
        }
        else if (node.isExplored)
        {
            _label.color = _exploredColor;
        }
        else
        {
            _label.color = _defaultColor;
        }
      
    }

    private void DisplayCoordinates()
    {
        if (_gridManager == null) { return; }
        

        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);

        _label.text = $"{_coordinates.x},{_coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _showingLabels = !_showingLabels;
            _label.enabled = _showingLabels;
        }
    }
}
