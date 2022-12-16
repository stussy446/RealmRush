using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform _weapon;

    private Transform _target;

    void Start()
    {
       _target =  FindObjectOfType<EnemyMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        _weapon.LookAt(_target);
    }
}
