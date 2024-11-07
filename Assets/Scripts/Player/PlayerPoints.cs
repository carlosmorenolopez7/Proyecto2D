using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public int points;
    public int vidas;


    void Start()
    {
        points = 0;
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public void QuitarVida()
    {
        this.vidas -= 1;
    }

    public int GetVidas()
    {
        return this.vidas;
    }

    public int GetPoints()
    {
        return points;
    }
}
