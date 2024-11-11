using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpController : MonoBehaviour
{
    public TextMeshProUGUI secretText; // Referencia al objeto de texto en el canvas
    public float displayDuration; // Duración en segundos para mostrar el texto

    // Start is called before the first frame update
    void Start()
    {
        secretText.gameObject.SetActive(false); // Asegúrate de que el texto esté desactivado al inicio
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para detectar colisiones
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            secretText.gameObject.SetActive(true); // Activa el texto en el canvas
            StartCoroutine(DeactivateTextAfterDelay(displayDuration)); // Inicia la corrutina para desactivar el texto
        }
    }

    // Corrutina para desactivar el texto después de un retraso
    IEnumerator DeactivateTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        secretText.gameObject.SetActive(false); // Desactiva el texto
        Destroy(gameObject); // Destruye el power-up después de desactivar el texto
    }
}