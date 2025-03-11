using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;  // 愛心的 Prefab (PNG 圖片)
    public Transform heartContainer;  // 愛心的容器
    public int maxHealth = 5;  // 玩家最大生命
    private int currentHealth;
    private GameObject[] hearts;  // 用來存儲生成的愛心

    void Start()
    {
        currentHealth = maxHealth;
        hearts = new GameObject[maxHealth];  // 初始化愛心容器

        // 隱藏原始愛心物件
        heartPrefab.SetActive(false);

        InitializeHearts();  // 初始化愛心顯示
    }

    // 初始化愛心顯示
    void InitializeHearts()
    {
        // 根據 maxHealth 顯示愛心
        for (int i = 0; i < maxHealth; i++)
        {
            // 生成愛心 clone 並添加到容器中
            hearts[i] = Instantiate(heartPrefab, heartContainer);
            hearts[i].SetActive(true);  // 顯示愛心
        }
    }

    // 當玩家受到傷害時，減少生命並更新愛心顯示
    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();  // 更新愛心顯示
        }
    }

    // 更新愛心顯示
    void UpdateHearts()
    {
        // 根據剩餘生命更新顯示的愛心數量
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);  // 顯示愛心
            }
            else
            {
                hearts[i].SetActive(false);  // 隱藏愛心
            }
        }
    }
}
