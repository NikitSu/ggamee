using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpikesAndBooksAnimation : MonoBehaviour
{
    public Transform spikes;
    public Transform outerBooks;
    public Transform innerBooks;

    public float rotationSpeed = 20f;
    public float acceleration = 20f;
    public float explosionForce = 1000f;
    public float explosionDelay = 1f;
    public float drag = 1f;
    public float gravityScale = 5f;
    
    public CanvasGroup fadeCanvasGroup;  // CanvasGroup для затемнения
    public float fadeDuration = 1f;      // Длительность затемнения
    public string nextSceneName;         // Имя следующей сцены

    private bool isAnimating = false;

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(AnimateRotationAndExplosion());
        }
    }

    IEnumerator AnimateRotationAndExplosion()
    {
        float currentSpeed = rotationSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < explosionDelay)
        {
            currentSpeed += acceleration * Time.deltaTime;
            spikes.Rotate(0, 0, currentSpeed * Time.deltaTime);
            outerBooks.Rotate(0, 0, currentSpeed * Time.deltaTime);
            innerBooks.Rotate(0, 0, -currentSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Transform spike in spikes)
        {
            Rigidbody2D rb = spike.gameObject.AddComponent<Rigidbody2D>();
            Vector2 direction = (spike.position - spikes.position).normalized;
            rb.AddForce(direction * explosionForce);
            rb.linearDamping = drag;
            rb.gravityScale = gravityScale;
        }

        foreach (Transform book in outerBooks)
        {
            Rigidbody2D rb = book.gameObject.AddComponent<Rigidbody2D>();
            Vector2 direction = (book.position - outerBooks.position).normalized;
            rb.AddForce(direction * explosionForce);
            rb.linearDamping = drag;
            rb.gravityScale = gravityScale;
        }

        foreach (Transform book in innerBooks)
        {
            Rigidbody2D rb = book.gameObject.AddComponent<Rigidbody2D>();
            Vector2 direction = (book.position - innerBooks.position).normalized;
            rb.AddForce(direction * explosionForce);
            rb.linearDamping = drag;
            rb.gravityScale = gravityScale;
        }

        // Ждем полсекунды перед началом затемнения
        yield return new WaitForSeconds(0.3f);

        // Начинаем затемнение экрана
        StartCoroutine(FadeOutAndLoadScene());
    }

    IEnumerator FadeOutAndLoadScene()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // Плавно увеличиваем альфа-канал до 1
            yield return null;
        }

        // Загружаем следующую сцену после завершения затемнения
        SceneManager.LoadScene(nextSceneName);
    }
}
