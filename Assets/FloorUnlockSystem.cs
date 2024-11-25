using UnityEngine;
using UnityEngine.UI;

public class FloorUnlockSystem : MonoBehaviour
{
    public GameObject unlockPanel; // Панель с затемнением перед третьим этажом
    public Button unlockButton; // Кнопка разблокировки третьего этажа
    public int unlockCost = 1000; // Стоимость разблокировки этажа

    void Start()
    {
        // Установка начальных значений
        unlockButton.onClick.AddListener(UnlockFloor);
    }

    void UnlockFloor()
    {
        // Проверка наличия достаточного количества средств (логика проверки будет зависеть от вашей системы ресурсов)
        if (PlayerResources.Money >= unlockCost)
        {
            PlayerResources.Money -= unlockCost;
            unlockPanel.SetActive(false); // Убираем затемненную панель, разблокируя этаж
        }
        else
        {
            Debug.Log("Недостаточно средств для разблокировки этажа.");
        }
    }
}

public static class PlayerResources
{
    public static int Money = 2000; // Пример ресурса, который игрок может использовать
}