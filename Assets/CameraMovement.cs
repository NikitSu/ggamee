using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Скорость перемещения камеры
    public float boundaryX = 10f;  // Граница по X
    public float boundaryY = 10f;  // Граница по Y
    public float inertiaDamping = 0.9f; // Коэффициент затухания инерции

    private Vector3 lastPosition;
    private Vector3 touchDelta;
    private Vector3 inertia = Vector3.zero; // Вектор для хранения инерции

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Получаем первое касание
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Сохраняем позицию, когда начинается касание
                lastPosition = Camera.main.ScreenToWorldPoint(touch.position);
                inertia = Vector3.zero; // Сбрасываем инерцию
            }

            if (touch.phase == TouchPhase.Moved)
            {
                // Вычисляем разницу позиции для перемещения камеры
                touchDelta = lastPosition - Camera.main.ScreenToWorldPoint(touch.position);
                
                // Перемещаем камеру с учетом скорости перемещения
                Vector3 newPosition = transform.position + touchDelta * moveSpeed;

                // Ограничиваем движение камеры в пределах границ
                newPosition.x = Mathf.Clamp(newPosition.x, -boundaryX, boundaryX);
                newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);

                transform.position = newPosition;

                // Обновляем инерцию на основе последнего движения
                inertia = touchDelta * moveSpeed;
                lastPosition = Camera.main.ScreenToWorldPoint(touch.position);
            }
        }
        else
        {
            // Применяем инерцию, если палец не касается экрана
            if (inertia.magnitude > 0.01f)
            {
                Vector3 newPosition = transform.position + inertia;

                // Ограничиваем движение камеры в пределах границ
                newPosition.x = Mathf.Clamp(newPosition.x, -boundaryX, boundaryX);
                newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);

                transform.position = newPosition;

                // Плавное затухание инерции
                inertia *= inertiaDamping;
            }
            else
            {
                inertia = Vector3.zero; // Останавливаем инерцию, когда она становится очень малой
            }
        }
    }
}
