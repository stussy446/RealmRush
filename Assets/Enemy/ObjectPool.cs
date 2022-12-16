using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnRate = 1f;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    
    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnRate);
            Instantiate(_enemyPrefab, transform);
        }
    }

}
