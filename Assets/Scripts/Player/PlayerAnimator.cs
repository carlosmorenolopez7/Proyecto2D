using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private bool corriendo = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimarJugador();
    }

    private void AnimarJugador()
    {
        animator.SetBool("TocandoSuelo", playerController.TocandoSuelo());
        if ((playerController.VelocidadJugadorX() > 1 ||
            playerController.VelocidadJugadorX() < -1) &&
            playerController.TocandoSuelo2())
        {
            corriendo = true;
            animator.SetBool("Corriendo", corriendo);
        }
        else if ((playerController.VelocidadJugadorX() <1 ||
            playerController.VelocidadJugadorX() > -1) &&
            playerController.TocandoSuelo2())
        {
            corriendo = false;
            animator.SetBool("Corriendo", corriendo);
        }
    }
}
