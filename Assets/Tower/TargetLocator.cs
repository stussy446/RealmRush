using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    [SerializeField] private float _range = 15f;
    [SerializeField] private ParticleSystem _projectileParticles;

    private Transform _target;

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _target = closestTarget;
    }

    private void AimWeapon()
    {
        if (_target == null) { return; }

        float targetDistance = Vector3.Distance(transform.position, _target.position);

        _weapon.LookAt(_target);

        if (targetDistance <= _range)
        {
            
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {

        var emissionModule = _projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
