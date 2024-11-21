using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource levelMusic; // Añade esta línea para la referencia al AudioSource

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            if ( GameIsPaused )
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        levelMusic.volume = 1f; // Restaura el volumen original
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        levelMusic.volume = 0.2f; // Baja el volumen de la música
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}