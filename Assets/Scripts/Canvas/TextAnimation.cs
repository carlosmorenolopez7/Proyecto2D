using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float animationDuration = 1f;
    public float scaleMultiplier = 1.2f;

    void Start()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        Vector3 originalScale = textMeshPro.transform.localScale;
        Vector3 targetScale = originalScale * scaleMultiplier;
        float elapsedTime = 0f;

        while (true)
        {
            // Agrandar
            while (elapsedTime < animationDuration)
            {
                textMeshPro.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;

            // Achicar
            while (elapsedTime < animationDuration)
            {
                textMeshPro.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
        }
    }
}