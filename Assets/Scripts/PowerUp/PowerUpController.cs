using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpController : MonoBehaviour
{
    public TextMeshProUGUI secretText; // Referencia al objeto de texto en el canvas
    public float displayDuration; // Duración en segundos para mostrar el texto

    void Start()
    {
        secretText.gameObject.SetActive(false);
    }

    // Método para detectar colisiones
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            secretText.gameObject.SetActive(true);
            StartCoroutine(DeactivateTextAfterDelay(displayDuration));
            GameManager.IncrementPowerUpCount();
        }
    }

    // Corrutina para desactivar el texto después de un retraso
    IEnumerator DeactivateTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        secretText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}