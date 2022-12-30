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


    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();
    private Waypoint _waypoint;
    private bool _showingLabels;


    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _label.enabled = _showingLabels;
        _waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
        UpdateObjectName();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void SetLabelColor()
    {
        _label.color = _waypoint.IsPlaceable ? _defaultColor : _blockedColor;
    }

    private void DisplayCoordinates()
    {
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

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
