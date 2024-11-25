using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomControl : MonoBehaviour
{
    public float zoomSpeed = 30.0f; // Скорость зума для колесика мыши и касания
    public float minZoom = 5f; // Минимальный уровень зума
    public float maxZoom = 20f; // Максимальный уровень зума

    void Update()
    {
        HandleMouseZoom();
        HandleTouchZoom();
    }

    void HandleMouseZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float newSize = Camera.main.orthographicSize - scroll * zoomSpeed * Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }

    void HandleTouchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            float newSize = Camera.main.orthographicSize - difference * zoomSpeed * 0.01f;
            Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}
