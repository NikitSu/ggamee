using UnityEngine;

public class LibraryCameraControl : MonoBehaviour
{
    public float inertiaDamping = 0.9f; // Коэффициент затухания инерции
    public float maxVelocity = 10f; // Максимальная скорость панорамирования
    private Vector3 lastTouchPosition; // Последняя позиция касания
    private Vector3 panVelocity; // Скорость для инерции
    private bool isDragging = false; // Проверка, идет ли текущее перетаскивание

    void Update()
    {
        // Проверка на касание
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                isDragging = true;
                panVelocity = Vector3.zero; // Сбрасываем скорость при начале касания
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchCurrentPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                Vector3 direction = lastTouchPosition - touchCurrentPosition;
                direction.z = 0; // Игнорируем ось Z

                // Перемещаем камеру с учетом направления
                transform.Translate(direction, Space.World);

                // Обновляем скорость для инерции
                panVelocity = direction / Time.deltaTime;

                // Обновляем последнюю позицию касания
                lastTouchPosition = touchCurrentPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false; // Останавливаем перетаскивание, включаем инерцию
            }
        }

        // Если палец отпущен, добавляем инерцию
        if (!isDragging && panVelocity.magnitude > 0.01f)
        {
            // Перемещаем камеру по направлению инерции
            transform.Translate(panVelocity * Time.deltaTime, Space.World);

            // Постепенно уменьшаем скорость
            panVelocity *= inertiaDamping;
        }
    }
}
