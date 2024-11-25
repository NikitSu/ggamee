using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public GameObject panel;
    public float panSpeed = 0.5f;
    public float inertiaDamping = 0.9f;
    private Vector3 touchStart;
    private Vector3 velocity;

    void Update()
    {
        // Проверяем, что панель не открыта
        if (panel != null && panel.activeSelf)
        {
            return; // Прерываем выполнение Update, если панель включена
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                velocity = Vector3.zero;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchCurrent = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                Vector3 direction = touchStart - touchCurrent;
                //direction.z = 0; // Игнорируем изменения по оси Z

                // Панорамируем только по x и y, так как камера наклонена
                transform.position += new Vector3(direction.x * panSpeed, direction.y * panSpeed, direction.z * panSpeed);
                velocity = new Vector3(direction.x * panSpeed / Time.deltaTime, direction.y * panSpeed / Time.deltaTime, direction.z * panSpeed / Time.deltaTime);

                // Обновляем начальную позицию
                touchStart = touchCurrent;
            }
        }
        else
        {
            // Эффект инерции
            if (velocity.magnitude > 0.01f)
            {
                transform.position += velocity * Time.deltaTime;
                velocity *= inertiaDamping;
            }
            else
            {
                velocity = Vector3.zero;
            }
        }
    }
}
