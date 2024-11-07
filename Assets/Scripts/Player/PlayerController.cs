using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public int velocidad;
    public Rigidbody2D rb;
    float x;
    public int fuerzaSalto;
    private bool tocaSuelo = true;
    private SpriteRenderer spriteRenderer;
    public AudioSource gameOverAudio;
    private bool finJuego = false;
    private PlayerPoints playerPoints;
    public bool vulnerable;

    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       finJuego = false;
       playerPoints = GetComponent<PlayerPoints>();
       vulnerable = true;
    }

    void Update()
    {
        if (FinalizadoJuego())
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            spriteRenderer.enabled = false;
            return;
        }
        x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo())
        {
            rb.AddForce(Vector2.up*fuerzaSalto, ForceMode2D.Impulse);
        }
        if (rb.velocity.x>0)
        {
            spriteRenderer.flipX = false;
        }
        else {
            if(rb.velocity.x<0)
            spriteRenderer.flipX = true;
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
        RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0,-2f,0), Vector2.down, 0.75f);
        return toca.collider != null;
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
                    spriteRenderer.color = Color.red;
                    vulnerable = false;
                    Invoke("HacerVulnerable", 1f);
                    playerPoints.QuitarVida();
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
        yield return new WaitForSeconds(2f);
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

}
