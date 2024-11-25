using UnityEngine;
using UnityEngine.UI;

public class OpenPanel : MonoBehaviour
{
    public Button button1;   // Ссылка на кнопку, по нажатию которой будет открываться панель
    public GameObject panel; // Ссылка на панель, которую нужно открыть
    public GameObject buttons;
    public Button backbut;
    void Start()
    {
        // Убедимся, что панель изначально скрыта
        panel.SetActive(false);

        // Добавляем слушатель к кнопке, чтобы вызвать метод OpenPanelOnClick при нажатии
        button1.onClick.AddListener(OpenPanelOnClick);
    }

    void OpenPanelOnClick()
    {
        // Показываем панель
        panel.SetActive(true);
        buttons.SetActive(false);
    }

    public void ClosePanelOnClick()
    {
        panel.SetActive(false);
        buttons.SetActive(true);
    }
}
