using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 150;
    [SerializeField] private int _currentBalance;

    public int CurrentBalance { get { return _currentBalance; } }

    private void Awake()
    {
        _currentBalance = _startingBalance;
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);

        if (_currentBalance < 0)
        {
            // lose the game 
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
