using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{

    public int velocidad;
    public Rigidbody2D rb;
    float x;
    public int fuerzaSalto;
    private bool tocaSuelo;
    private SpriteRenderer spriteRenderer;
    public AudioSource gameOverAudio;
    private bool finJuego = false;
    private PlayerPoints playerPoints;
    public bool vulnerable;
    private Animator animator;
    public float fallThreshold;
    public static event Action OnLifeLost;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        finJuego = false;
        playerPoints = GetComponent<PlayerPoints>();
        vulnerable = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (FinalizadoJuego())
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Die();
            return;
        }

        x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo())
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Verificar si el jugador ha caído por debajo del umbral
        if (transform.position.y < fallThreshold)
        {
            // Si el jugador cae por debajo del umbral, desencadenar la muerte
            if (!finJuego)
            {
                finJuego = true;
                StartCoroutine(CorrutinaFinJuego());
            }
        }
    }

    private void FixedUpdate()
    {
        if (FinalizadoJuego())
        {
            return;
        }
        rb.velocity = new Vector2 (x*velocidad*Time.fixedDeltaTime, rb.velocity.y);
    }
    public bool TocandoSuelo()
    {
        RaycastHit2D[] tocan = Physics2D.RaycastAll(transform.position + new Vector3(0, -2f, 0), Vector2.down, 0.75f);
        foreach (RaycastHit2D toca in tocan)
        {
            if (toca.collider != null && toca.collider.CompareTag("Tilemap"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,-2f,0) * 0.75f);
    }

    public float VelocidadJugadorX()
    {
        return rb.velocity.x;
    }

    public float VelocidadJugadorY()
    {
        return rb.velocity.y;
    }

    public bool TocandoSuelo2()
    {
        return tocaSuelo;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "enemy")
        {
            if (vulnerable)
            {
                animator.SetTrigger("Hurt");
                spriteRenderer.color = Color.red;
                vulnerable = false;
                Invoke("HacerVulnerable", 0.75f);
                playerPoints.QuitarVida();
                OnLifeLost?.Invoke();
                if (playerPoints.GetVidas() == 0)
                {
                    finJuego = true;
                    gameOverAudio.Play();
                    StartCoroutine(CorrutinaFinJuego());
                }
            }
        }
        tocaSuelo = true;
    }

    private void HacerVulnerable()
    {
        vulnerable = true;
        spriteRenderer.color = Color.white;
    }

    IEnumerator CorrutinaFinJuego()
    {
        yield return new WaitForSeconds(3f);
        FinJuego();
    }

    private void FinJuego()
    {
        gameOverAudio.Play();
        SceneManager.LoadScene("FinJuego");
    }

    public bool FinalizadoJuego()
    {
        return finJuego;
    }

    private void OnCollisionExit2D(Collision2D collision) 
    {
        tocaSuelo = false;
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        rb.GetComponent<Collider2D>().enabled = false;
    }

}
