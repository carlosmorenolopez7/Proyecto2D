using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private SpriteRenderer sprite;
    private Animator animator;
    private bool atacando;
    private PlayerController playerController;
    public Transform attackPoint;
    public Transform attackPoint2;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Start()
    {
        atacando = false;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1") && !atacando && playerController.TocandoSuelo2())
            {
                Atacar();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void Atacar()
    {
        atacando = true;
        animator.SetBool("Atacando", atacando);
        StartCoroutine("Atacando");
        if (sprite.flipX)
        {
            attackPoint2.gameObject.SetActive(false);
        }
        else
        {
            attackPoint.gameObject.SetActive(false);
        }
        Collider2D[] hitEnemies = sprite.flipX ? 
            Physics2D.OverlapCircleAll(attackPoint2.position, attackRange, enemyLayers) : 
            Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
        attackPoint.gameObject.SetActive(true);
        attackPoint2.gameObject.SetActive(true);
    }

    void OnDrawGizmos()
    {
        if (attackPoint && attackPoint2 == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPoint2.position, attackRange);
    }

    IEnumerator Atacando()
    {
        yield return new WaitForSeconds(0.6f);
        atacando = false;
        animator.SetBool("Atacando", atacando);
    }
}
