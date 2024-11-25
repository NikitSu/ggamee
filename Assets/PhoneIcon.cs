using UnityEngine;

public class PhoneIcon : MonoBehaviour
{
    public GameObject popupPanel;  // Панель с интерфейсом магазина

    // Метод для открытия/закрытия панели
    public void TogglePopupPanel()
    {
        popupPanel.SetActive(!popupPanel.activeSelf);  // Переключение активности панели
    }
}
