using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionToggle : MonoBehaviour
{
    public GameObject answerPanel;
    private bool isExpanded = false;
    private float animationDuration = 0.3f;
    private float answerOffset = 30f; // Отступ между вопросом и ответом

    public void ToggleAnswer()
    {
        if (isExpanded)
            StartCoroutine(Collapse());
        else
            StartCoroutine(Expand());
    }

    private IEnumerator Expand()
    {
        isExpanded = true;
        float elapsed = 0f;
        float startHeight = 0f;
        float endHeight = 100f;

        answerPanel.SetActive(true);
        answerPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -answerOffset);

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float newHeight = Mathf.Lerp(startHeight, endHeight, elapsed / animationDuration);
            answerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(answerPanel.GetComponent<RectTransform>().sizeDelta.x, newHeight);
            yield return null;
        }
    }

    private IEnumerator Collapse()
    {
        isExpanded = false;
        float elapsed = 0f;
        float startHeight = 100f;
        float endHeight = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float newHeight = Mathf.Lerp(startHeight, endHeight, elapsed / animationDuration);
            answerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(answerPanel.GetComponent<RectTransform>().sizeDelta.x, newHeight);
            yield return null;
        }

        answerPanel.SetActive(false);
    }
}
