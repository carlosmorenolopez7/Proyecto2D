using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    public GameObject player;
    public Animator animator;
    private int currentHp;
    private CapsuleCollider2D enemyCollider;
    public int maxHp = 100;
    public float detectionRange;
    public float attackRange;
    private Vector2 originalColliderSize;

    void Start()
    {
        currentHp = maxHp;
        transform.localScale = new Vector3(1, 1, 1);
        player = GameObject.FindGameObjectWithTag("Player");
        enemyCollider = GetComponent<CapsuleCollider2D>();
        originalColliderSize = enemyCollider.size;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (chase)
        {
            Chase();
        }
        else
        {
            ReturnToStartingPoint();
        }     
        Flip();
        CheckForPlayer();
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
            // Agrandar solo la dimensión x del collider
            enemyCollider.size = new Vector2(originalColliderSize.x * 2f, originalColliderSize.y);
            
            animator.SetTrigger("Attack");
            // Restaurar el tamaño original del collider después del ataque
            StartCoroutine(RestoreColliderSize());
        }
    }

    IEnumerator RestoreColliderSize()
    {
        // Esperar un tiempo antes de restaurar el tamaño original
        yield return new WaitForSeconds(4f);
        enemyCollider.size = originalColliderSize;
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
        {
            chase = false;
        }
        else
        {
            chase = true;
        }
    }

    private void ReturnToStartingPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x < player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        animator.SetBool("Dead", true);
        rb.GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.enabled = false;
    }
}
