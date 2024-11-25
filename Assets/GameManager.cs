using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentMoney = 10000; // Баланс пользователя

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
            LoadMoney(); // Загружаем баланс при старте
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        SaveMoney(); // Сохраняем баланс
    }

    public bool SubtractMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            SaveMoney(); // Сохраняем баланс
            return true;
        }
        else
        {
            Debug.Log("Not enough money!");
            return false;
        }
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", currentMoney); // Сохраняем баланс в PlayerPrefs
        PlayerPrefs.Save(); // Принудительное сохранение
    }

    private void LoadMoney()
    {
        currentMoney = PlayerPrefs.GetInt("Money", 10000); // Загружаем баланс из PlayerPrefs
    }
}
