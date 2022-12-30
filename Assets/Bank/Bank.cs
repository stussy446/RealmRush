using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 150;
    [SerializeField] private int _currentBalance;
    [SerializeField] private TMP_Text _balanceText;

    public int CurrentBalance { get { return _currentBalance; } }

    private void Awake()
    {
        _currentBalance = _startingBalance;
        UpdateBalanceText();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        UpdateBalanceText();
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);

        if (_currentBalance < 0)
        {
            // lose the game 
            ReloadScene();
        }

        UpdateBalanceText();
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void UpdateBalanceText()
    {
        _balanceText.text = $"GOLD: {_currentBalance}";
    }
}
