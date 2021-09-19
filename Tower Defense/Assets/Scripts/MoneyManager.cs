using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int currentPlayerMoney;
    [SerializeField] public int startingMoney;

    public int GetCurrentMoney()
    {
        return currentPlayerMoney;
    }

    public void AddMoney(int amount)
    {
        currentPlayerMoney += amount;
    }
    public void RemoveMoney(int amount)
    {
        currentPlayerMoney -= amount;
    }

    public void Start()
    {
        currentPlayerMoney = startingMoney;
    }
}
