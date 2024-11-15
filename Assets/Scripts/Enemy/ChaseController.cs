using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{
    public FlyingEnemy[] enemies;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemies)
            {
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemies)
            {
                enemy.chase = false;
            }
        }
    }
}
