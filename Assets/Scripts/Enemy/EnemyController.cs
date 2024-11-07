using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject player;  // Referencia al GameObject del jugador
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public Animator animator;
    public int maxHp = 100;
    private int currentHp;
    private bool movingRight = true;

    void Start()
    {
        currentHp = maxHp;
        transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        Move();
        CheckForPlayer();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tilemap"))
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Si el jugador está dentro del rango de detección, intentamos atacar
        if (distanceToPlayer <= detectionRange)
        {
            Attack(distanceToPlayer); // Llamamos a la función de ataque con la distancia
        }
    }

    void Attack(float distanceToPlayer)
    {
        if (distanceToPlayer <= attackRange)
        {
            animator.SetTrigger("Attack");
            Debug.Log("Ataque realizado.");
        }
        else
        {
            Debug.Log("Jugador fuera del rango de ataque.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        animator.SetTrigger("Hurt");
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}