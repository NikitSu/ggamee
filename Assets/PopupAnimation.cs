using UnityEngine;
using System.Collections;

public class PopupAnimation : MonoBehaviour
{
    public GameObject popupPanel;          // Панель всплывающего окна
    public GameObject phoneButton;         // Кнопка телефона
    public GameObject backgroundOverlay;   // Затемненный фон за окном
    public float animationDuration = 0.2f; // Длительность анимации

    public static bool IsPopupOpen { get; private set; } // Статус панели

    private Vector3 originalScale;

    void Start()
    {
        originalScale = popupPanel.transform.localScale;
        popupPanel.transform.localScale = Vector3.zero; 
        popupPanel.SetActive(false);
        backgroundOverlay.SetActive(false);
        IsPopupOpen = false; // Поначалу панель закрыта
    }

    public void OpenPopup()
    {
        phoneButton.SetActive(false);
        backgroundOverlay.SetActive(true);
        popupPanel.SetActive(true);
        IsPopupOpen = true; // Устанавливаем флаг, что панель открыта
        StartCoroutine(AnimatePopup(true));
    }

    public void ClosePopup()
    {
        StartCoroutine(AnimatePopup(false));
    }

    IEnumerator AnimatePopup(bool opening)
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = popupPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = popupPanel.AddComponent<CanvasGroup>();
        }

        Vector3 startScale = opening ? Vector3.zero : originalScale;
        Vector3 endScale = opening ? originalScale : Vector3.zero;
        float startAlpha = opening ? 0f : 1f;
        float endAlpha = opening ? 1f : 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            popupPanel.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);

            yield return null;
        }

        popupPanel.transform.localScale = endScale;
        canvasGroup.alpha = endAlpha;

        if (!opening)
        {
            popupPanel.SetActive(false);
            backgroundOverlay.SetActive(false);
            phoneButton.SetActive(true);
            IsPopupOpen = false; // Сбрасываем флаг, когда панель закрыта
        }
    }
}
