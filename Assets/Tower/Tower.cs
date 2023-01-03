using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _cost = 75;
    [SerializeField] private float _buildDelay = 1f;


    private void Start()
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
    {
        Debug.Log("hey");
        TurnOffChildren();

        var children = GetComponentInChildren<Transform>();

        foreach (Transform child in children)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(_buildDelay);

            var grandchildren = GetComponentInChildren<Transform>();
            foreach (Transform grandchild in grandchildren)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

    private void TurnOffChildren()
    {
        var children = GetComponentInChildren<Transform>();

        foreach (Transform child in children)
        {
            child.gameObject.SetActive(false);
            var grandchildren = GetComponentInChildren<Transform>();

            foreach (Transform grandchild in grandchildren)
            {
                grandchild.gameObject.SetActive(false);
            }
        }
    }

    internal bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) { return false; }

        if (bank.CurrentBalance >= _cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(_cost);
            return true;
        }

        return false;
    }
}
