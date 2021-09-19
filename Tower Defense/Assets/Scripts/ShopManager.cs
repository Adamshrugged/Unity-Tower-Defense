using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] public MoneyManager moneyManager;

    // Basic tower
    [SerializeField] public GameObject basicTowerPrefab;
    [SerializeField] public int basicTowerCost;

    public int GetTowerCost(GameObject towerPrefab)
    {
        int cost = 0;
        if(towerPrefab == basicTowerPrefab)
        {
            cost = basicTowerCost;
        }
        return cost;
    }

    public void buyTower( GameObject towerPrefab)
    {
        moneyManager.RemoveMoney(GetTowerCost(towerPrefab));
    }

    public bool CanBuyTower(GameObject towerPrefab)
    {
        if ( moneyManager.GetCurrentMoney() >= GetTowerCost(towerPrefab) )
        {
            return true;
        }

        return false;
    }

}
