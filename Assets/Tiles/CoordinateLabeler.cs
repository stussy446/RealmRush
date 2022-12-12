using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();
    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
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
}
