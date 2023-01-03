using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] private float _speed = 1f;

    private List<Node> _path = new List<Node>();
    private Enemy _enemy;
    private GridManager _gridManager;
    private PathFinder _pathFinder;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinder = FindObjectOfType<PathFinder>();
    }

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        _path.Clear();
        _path = _pathFinder.GetNewPath();
    }

    private void ReturnToStart()
    {
        transform.position = _gridManager.GetPositionFromCoordinates(_pathFinder.StartCoordinates);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = _gridManager.GetPositionFromCoordinates(_path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        _enemy.StealGold();
        gameObject.SetActive(false);
    }
}
