using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Vector2Int _startCoordinates;
    [SerializeField] private Vector2Int _destinationCoordinates;

    private Node _currentSearchNode;
    private Node _startNode;
    private Node _destinationNode;

    private Vector2Int[] _directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    private GridManager _gridManager;

    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    private Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();
    private Queue<Node> _frontier = new Queue<Node>();

    public Vector2Int StartCoordinates { get { return _startCoordinates; } }
    public Vector2Int DestinationCoordinates { get { return _destinationCoordinates; } }
    
    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();

        if (_gridManager != null)
        {
            _grid = _gridManager.Grid;
            _startNode = _grid[_startCoordinates];
            _destinationNode = _grid[_destinationCoordinates];
        }      
    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        _gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
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
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (neighbor.isWalkable && !_reached.ContainsKey(neighbor.coordinates))
            {
                neighbor.connectedTo = _currentSearchNode;
                _reached.Add(neighbor.coordinates, neighbor);
                _frontier.Enqueue(neighbor);
            }
        }
    }

    private void BreadthFirstSearch()
    {
        _startNode.isWalkable = true;
        _destinationNode.isWalkable = true;

        _frontier.Clear();
        _reached.Clear();

        bool is_running = true;

        _frontier.Enqueue(_startNode);
        _reached.Add(_startCoordinates, _startNode);

        while (_frontier.Count > 0 && is_running)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (_currentSearchNode.coordinates == _destinationCoordinates)
            {
                is_running = false;
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = _destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            bool previousState = _grid[coordinates].isWalkable;

            _grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            _grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }
}
