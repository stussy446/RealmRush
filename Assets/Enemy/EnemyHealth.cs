using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints = 5;

    private int _currentHitPoints = 0;

    // Start is called before the first frame update
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
        Debug.Log("ow");
        _currentHitPoints--;

        if (_currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
