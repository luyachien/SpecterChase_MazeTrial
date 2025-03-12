using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 用於場景切換

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab; // 愛心的 Prefab (PNG 圖片)
    public Transform heartContainer; // 愛心 UI 的父物件
    public GameObject gameOverPanel; // 遊戲結束的 UI

    public int maxHealth = 5; // 最大生命值
    private int currentHealth;
    private GameObject[] hearts; // 存放所有愛心的陣列

    void Start()
    {
        currentHealth = maxHealth;
        hearts = new GameObject[maxHealth]; // 初始化愛心陣列
        heartPrefab.SetActive(false); // 隱藏原始的 heartPrefab
        InitializeHearts(); // 產生愛心 UI
        gameOverPanel.SetActive(false); // 一開始隱藏遊戲結束畫面
    }

    void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i] = Instantiate(heartPrefab, heartContainer);
            hearts[i].SetActive(true);
        }
    }

    // 玩家受到傷害時
    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            Debug.Log("當前生命值：" + currentHealth);
            UpdateHearts();

            if (currentHealth <= 0)
            {
                GameOver(); // 生命值歸零，遊戲結束
            }
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }

    // 遊戲結束
    void GameOver()
    {
        Debug.Log("遊戲結束！");
        SceneManager.LoadScene("GameOverScene"); // 直接切換到遊戲結束畫面
    }

}
