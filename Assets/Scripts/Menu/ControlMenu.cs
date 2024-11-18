using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public void BotonJugar()
    {
        SceneManager.LoadScene("Bosque Perdido");
    }

    public void BotonSalir()
    {
        Application.Quit();
    }

    public void BotonCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void BotonSalirCreditos()
    {
        SceneManager.LoadScene("Menu");
    }
}
