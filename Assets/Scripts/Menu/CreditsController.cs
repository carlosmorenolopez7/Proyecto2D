using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsController : MonoBehaviour
{
    public TextMeshProUGUI secretsText;

    void Start()
    {
        secretsText.text = "Secretos obtenidos: " + GameManager.PowerUpCount + "/3";
    }
}