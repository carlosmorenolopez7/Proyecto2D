using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject player;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public Animator animator;
    public int maxHp = 100;
    private int currentHp;
    private bool movingRight = true;
    private CapsuleCollider2D enemyCollider;
    private Vector2 originalColliderSize;

    void Start()
    {
        currentHp = maxHp;
        transform.localScale = new Vector3(1, 1, 1);
        enemyCollider = GetComponent<CapsuleCollider2D>();
        originalColliderSize = enemyCollider.size;
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
            Attack(distanceToPlayer);
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