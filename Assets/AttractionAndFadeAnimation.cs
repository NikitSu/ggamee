using UnityEngine;
using System.Collections;

public class AttractionAndFadeAnimation : MonoBehaviour
{
    public Transform target;       // Точка, к которой объекты будут притягиваться (центр кнопки "Play")
    public Transform[] objects;    // Массив объектов (шипы и книжки), которые будут анимированы
    public float attractionSpeed = 5f;      // Скорость притяжения к центру
    public float fadeSpeed = 1f;            // Скорость исчезновения объектов
    public float disappearDelay = 0.5f;     // Задержка перед началом исчезновения после достижения центра

    private bool isAnimating = false;

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(AnimateAttractionAndFade());
        }
    }

    IEnumerator AnimateAttractionAndFade()
    {
        // Притягиваем объекты к центру
        bool allReached = false;
        while (!allReached)
        {
            allReached = true;
            foreach (Transform obj in objects)
            {
                // Проверка, что объект существует и его можно переместить
                if (obj != null)
                {
                    // Перемещение объекта к центру
                    obj.position = Vector3.MoveTowards(obj.position, target.position, attractionSpeed * Time.deltaTime);

                    // Проверка, достиг ли объект центра
                    if (Vector3.Distance(obj.position, target.position) > 0.1f)
                    {
                        allReached = false;
                    }
                }
            }
            yield return null;
        }

        // Ждем немного перед исчезновением
        yield return new WaitForSeconds(disappearDelay);

        // Исчезновение объектов (плавное изменение прозрачности)
        float fadeAmount = 1f;
        while (fadeAmount > 0)
        {
            fadeAmount -= fadeSpeed * Time.deltaTime;
            foreach (Transform obj in objects)
            {
                if (obj != null)
                {
                    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        Color color = sr.color;
                        color.a = Mathf.Clamp01(fadeAmount);
                        sr.color = color;
                    }
                }
            }
            yield return null;
        }

        // Полностью скрываем объекты
        foreach (Transform obj in objects)
        {
            if (obj != null)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
