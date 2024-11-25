using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public Text moneyText; // Текст, отображающий количество денег
    public int money = 10000; // Начальное количество денег у игрока

    void Start()
    {
        LoadMoney(); // Загружаем деньги при запуске игры
        UpdateMoneyUI(); // Обновляем интерфейс
    }

    public void AddMoney(int amount)
    {
        money += amount;
        SaveMoney(); // Сохраняем деньги после изменения
        UpdateMoneyUI();
    }

    public void SubtractMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            SaveMoney(); // Сохраняем деньги после изменения
            UpdateMoneyUI();
        }
        else
        {
            Debug.Log("Недостаточно средств!");
        }
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "$" + money.ToString();
    }

    void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", money); // Сохраняем текущую сумму денег
        PlayerPrefs.Save(); // Принудительно сохраняем данные
    }

    void LoadMoney()
    {
        money = PlayerPrefs.GetInt("Money", 10000); // Загружаем сохраненные деньги (или 10000, если сохранений нет)
    }
}
