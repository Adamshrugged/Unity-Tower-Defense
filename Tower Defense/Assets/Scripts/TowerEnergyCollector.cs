using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnergyCollector : MonoBehaviour
{
    // Connect to default variables
    [SerializeField] public int energyYield = 0;
    [SerializeField] public float energyTimer = 0f;

    private float countdownTimer = 0f;

    private void Start()
    {
        countdownTimer = energyTimer;
    }

    private void Update()
    {
        countdownTimer -= Time.deltaTime;
        if(countdownTimer <= 0f)
        {
            PlayerStats.energy += energyYield;
            countdownTimer = energyTimer;
        }
    }
}
