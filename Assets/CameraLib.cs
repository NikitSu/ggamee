using UnityEngine;

public class CameraLib : MonoBehaviour
{
    public GameObject panel;
    public float dragSpeed = 0.01f; // Скорость перемещения камеры
    public float zoomSpeedTouch = 0.1f; // Скорость зума для сенсорного ввода
    public float zoomSpeedMouse = 10f; // Скорость зума для колесика мыши
    public float minZoom = 5f; // Минимальный уровень зума
    public float maxZoom = 20f; // Максимальный уровень зума
    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
         if (panel != null && panel.activeSelf)
        {
            return; // Прерываем выполнение Update, если панель включена
        }
        HandleTouchInput();
        HandleMouseInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector3 currentTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                Vector3 difference = dragOrigin - currentTouchPosition;
                Vector3 move = new Vector3(0, -difference.y * dragSpeed, 0); // Инвертируем движение
                transform.Translate(move, Space.World);
                dragOrigin = currentTouchPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            ZoomCamera(-difference * zoomSpeedTouch);
        }
    }

    void HandleMouseInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll * zoomSpeedMouse);
    }

    void ZoomCamera(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
    }
}