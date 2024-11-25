using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Slider progressBar;         // Слайдер для отображения прогресса загрузки
    public string targetSceneName;     // Имя целевой сцены для загрузки
    public float fillSpeed = 0.5f;     // Скорость заполнения слайдера для плавной анимации

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(targetSceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float targetProgress = 0f; // Целевой прогресс для анимации

        while (!operation.isDone)
        {
            // Обновляем фактический прогресс загрузки
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Плавно интерполируем значение прогресса
            targetProgress = progress;
            while (progressBar.value < targetProgress)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, targetProgress, fillSpeed * Time.deltaTime);
                yield return null;
            }

            // Проверка завершения загрузки
            if (operation.progress >= 0.9f)
            {
                // Если загрузка завершена, ждем немного и активируем сцену
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
