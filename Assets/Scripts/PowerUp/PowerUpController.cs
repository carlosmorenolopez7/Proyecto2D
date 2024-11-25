using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpController : MonoBehaviour
{
    public TextMeshProUGUI secretText;
    public float displayDuration;

    void Start()
    {
        secretText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            secretText.gameObject.SetActive(true);
            StartCoroutine(DeactivateTextAfterDelay(displayDuration));
            GameManager.IncrementPowerUpCount();
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.PlayPowerUpSound();
            }
        }
    }

    IEnumerator DeactivateTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        secretText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}