using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FAQItem : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
    private bool isExpanded = false;
    private float animationDuration = 0.3f;

    private void Start()
    {
        answerText.gameObject.SetActive(false);
    }

    public void ToggleAnswer()
    {
        isExpanded = !isExpanded;
    answerText.gameObject.SetActive(isExpanded);

    // Если используется Layout Group, то вызовем перестроение макета
    LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

    }

    public void CloseAnswer()
    {
        if (isExpanded)
        {
            isExpanded = false;
            StopAllCoroutines();
            StartCoroutine(AnimateAnswer(false));
        }
    }

    private IEnumerator AnimateAnswer(bool expand)
    {
        answerText.gameObject.SetActive(true);
        float targetHeight = expand ? answerText.preferredHeight : 0f;
        float startHeight = answerText.rectTransform.sizeDelta.y;

        float time = 0f;
        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float height = Mathf.Lerp(startHeight, targetHeight, time / animationDuration);
            answerText.rectTransform.sizeDelta = new Vector2(answerText.rectTransform.sizeDelta.x, height);
            yield return null;
        }

        answerText.rectTransform.sizeDelta = new Vector2(answerText.rectTransform.sizeDelta.x, targetHeight);
        if (!expand) answerText.gameObject.SetActive(false);
    }
}
