using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] private int _difficultyRamp = 1;

    private int _currentHitPoints = 0;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _currentHitPoints = _maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;

        if (_currentHitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _enemy.RewardGold();
        _maxHitPoints += _difficultyRamp;
        gameObject.SetActive(false);
    }
}
