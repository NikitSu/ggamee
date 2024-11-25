using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BuildingTouchController : MonoBehaviour
{
    public GameObject panel;
    public string buildingName; // Название здания (для отображения на панели)
    public GameObject infoPanel; // Привязанная панель с информацией
    public GameObject rotatingCircle; // Привязанный вращающийся круг
    public string SceneName;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f; // Порог для двойного касания
    private bool isSelected = false;

    // Метод для привязки к OnClick() события кнопки здания
    public void OnBuildingClick()
    {
        if (panel != null && panel.activeSelf)
        {
            return; // Прерываем выполнение Update, если панель включена
        }
        // Определяем, является ли это двойным кликом
        if (Time.time - lastTapTime < doubleTapThreshold)
        {
            // Переход на другую сцену при двойном касании
            SceneManager.LoadScene(SceneName); // Укажите имя новой сцены
        }
        else
        {
            // Одиночный клик
            HandleSingleTap();
        }
        lastTapTime = Time.time;
    }

    private void HandleSingleTap()
    {
        if (isSelected)
        {
            DeselectBuilding();
        }
        else
        {
            DeselectOtherBuildings();
            SelectBuilding();
        }
    }

    private void SelectBuilding()
    {
        isSelected = true;
        
        // Активируем панель с информацией и устанавливаем текст названия
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<UnityEngine.UI.Text>().text = buildingName;

        // Активируем вращающийся круг и запускаем его вращение
        rotatingCircle.SetActive(true);
        StartCoroutine(RotateCircle(rotatingCircle));
    }

    private void DeselectBuilding()
    {
        isSelected = false;
        
        // Деактивируем панель информации и вращающийся круг
        infoPanel.SetActive(false);
        rotatingCircle.SetActive(false);
    }

    private void DeselectOtherBuildings()
    {
        foreach (var building in FindObjectsOfType<BuildingTouchController>())
        {
            if (building != this)
            {
                building.DeselectBuilding();
            }
        }
    }

    private IEnumerator RotateCircle(GameObject circle)
    {
        while (isSelected)
        {
            circle.transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
            yield return null;
        }
    }
}
