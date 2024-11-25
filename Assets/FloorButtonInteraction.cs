using UnityEngine;
using UnityEngine.UI;

public class FloorButtonInteraction : MonoBehaviour
{
    public GameObject floor;           // Объект этажа
    public Camera roomCamera;          // Камера, связанная с этажом
    public Button returnButton;        // Кнопка для возврата камеры
    public GameObject wall;            // Стена, которую нужно активировать/деактивировать

    private Camera mainCamera;
    private float rotationSpeed = 0.05f; // Скорость вращения камеры
    private float minRotation = -30f;   // Минимальный угол вращения
    private float maxRotation = 30f;    // Максимальный угол вращения
    private float currentRotation = 0f;
    private Vector2 touchStartPosition;
    private bool isDragging = false;

    void Start()
    {
        // Сохраняем ссылку на основную камеру
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is missing.");
        }

        // Убедитесь, что все камеры, кроме главной, выключены
        if (roomCamera != null)
        {
            roomCamera.gameObject.SetActive(false);
        }

        // Назначаем действие на кнопку возврата
        if (returnButton != null)
        {
            returnButton.onClick.AddListener(ReturnToMainCamera);
            returnButton.gameObject.SetActive(false); // Скрываем кнопку возврата в начале
        }

        // Убедитесь, что стена отключена в начале
        if (wall != null)
        {
            wall.SetActive(false);
        }
    }

    void Update()
    {
        if (roomCamera != null && roomCamera.gameObject.activeSelf)
        {
            HandleTouchRotation();
        }
    }

    void HandleTouchRotation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                float rotationAmount = -touchDeltaPosition.x * rotationSpeed;
                float newRotation = currentRotation + rotationAmount;

                if (newRotation >= minRotation && newRotation <= maxRotation)
                {
                    currentRotation = newRotation;
                    roomCamera.transform.Rotate(Vector3.up, rotationAmount, Space.World);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }

    public void SwitchToRoomCamera() // Сделано публичным, чтобы можно было привязать к кнопке
    {
        if (roomCamera != null && mainCamera != null)
        {
            Debug.Log("Switching to room camera for floor: " + floor.name);
            mainCamera.gameObject.SetActive(false); // Отключаем основную камеру
            roomCamera.gameObject.SetActive(true);   // Включаем камеру этажа

            if (wall != null)
            {
                wall.SetActive(true); // Активируем стену
            }

            if (returnButton != null)
            {
                returnButton.gameObject.SetActive(true); // Показываем кнопку возврата
            }
        }
        else
        {
            Debug.LogError("Room camera or Main Camera is missing.");
        }
    }

    public void ReturnToMainCamera() // Сделано публичным, чтобы можно было привязать к кнопке
    {
        if (mainCamera != null && roomCamera != null)
        {
            Debug.Log("Returning to main camera.");
            roomCamera.gameObject.SetActive(false); // Отключаем камеру этажа
            mainCamera.gameObject.SetActive(true);  // Включаем основную камеру

            if (wall != null)
            {
                wall.SetActive(false); // Деактивируем стену
            }

            if (returnButton != null)
            {
                returnButton.gameObject.SetActive(false); // Скрываем кнопку возврата
            }
        }
        else
        {
            Debug.LogError("Main Camera or Room Camera is missing.");
        }
    }
}
