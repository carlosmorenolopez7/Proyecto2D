using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static int powerUpCount = 0;

    public static int PowerUpCount
    {
        get { return powerUpCount; }
    }

    public static void IncrementPowerUpCount()
    {
        powerUpCount++;
    }
}