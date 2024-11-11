using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlHud : MonoBehaviour
{
    public GameObject[] healthSquares;
    public TextMeshProUGUI levelNameText;
    private Image[] healthImages;
    private int currentHealth;
    private string[] levelNames = { "Bosque Perdido", "Montaña Nívea", "Pantano Muerto" };

    void Start()
    {
        currentHealth = healthSquares.Length;
        // Inicializar el array de imágenes
        healthImages = new Image[healthSquares.Length];
        for (int i = 0; i < healthSquares.Length; i++)
        {
            healthImages[i] = healthSquares[i].GetComponent<Image>();
        }
        PlayerController.OnLifeLost += LoseLife;
        // Mostrar el nombre del nivel al iniciar
        int currentLevelIndex = GetCurrentLevelIndex(); // Obtén el índice del nivel actual
        if (currentLevelIndex >= 0 && currentLevelIndex < levelNames.Length)
        {
            StartCoroutine(ShowLevelName(levelNames[currentLevelIndex], 3f));
        }
        else
        {
            Debug.LogError("El índice del nivel actual está fuera de los límites del array levelNames.");
        }
    }

    void OnDestroy()
    {
        PlayerController.OnLifeLost -= LoseLife;
    }

    public void LoseLife()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHealth();
        }
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            healthImages[i].color = (i < currentHealth) ? Color.red : Color.black;
        }
    }

    private IEnumerator ShowLevelName(string levelName, float duration)
    {
        levelNameText.text = levelName;
        yield return StartCoroutine(FadeTextToFullAlpha(1f, levelNameText));
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(FadeTextToZeroAlpha(1f, levelNameText));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.gameObject.SetActive(true);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
            yield return null;
        }
        text.gameObject.SetActive(false);
    }

    private int GetCurrentLevelIndex()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        for (int i = 0; i < levelNames.Length; i++)
        {
            if (levelNames[i] == currentSceneName)
            {
                return i;
            }
        }
        return -1;
    }
}