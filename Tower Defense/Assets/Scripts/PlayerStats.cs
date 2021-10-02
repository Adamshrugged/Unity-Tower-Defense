using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int energy;
    public int startEnergy = 500;

    public static int lives;
    public int startLives = 100;

    private void Awake()
    {
        energy = startEnergy;
        lives = startLives;
    }
}
