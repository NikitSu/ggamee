using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; // CanvasGroup для плавного исчезновения
    public float fadeDuration = 1f;     // Длительность затухания

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        // Изначально устанавливаем CanvasGroup на 1 (полностью черный экран)
        fadeCanvasGroup.alpha = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration)); // Плавно уменьшаем альфа-канал
            yield return null;
        }

        // В конце устанавливаем альфа-канал на 0 для полной прозрачности
        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.gameObject.SetActive(false); // Отключаем CanvasGroup, чтобы освободить ресурсы
    }
}
