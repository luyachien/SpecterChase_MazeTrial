using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;  // 愛心的 Prefab (PNG 圖片)
    public Transform heartContainer;  // 愛心 UI 的父物件
    public int maxHealth = 5;  // 最大生命值
    private int currentHealth;
    private GameObject[] hearts;  // 存放所有愛心的陣列

    void Start()
    {
        currentHealth = maxHealth;
        hearts = new GameObject[maxHealth];  // 初始化愛心陣列

        // 隱藏原始的 heartPrefab
        heartPrefab.SetActive(false);

        InitializeHearts();  // 產生愛心 UI
    }

    // 初始化愛心 UI
    void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i] = Instantiate(heartPrefab, heartContainer); // 複製愛心 Prefab
            hearts[i].SetActive(true);  // 顯示愛心
        }
    }

    // 玩家受到傷害時，減少生命值並更新 UI
    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            Debug.Log("當前生命值：" + currentHealth);
            UpdateHearts();
        }
    }

    // 更新愛心 UI
    void UpdateHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i].SetActive(i < currentHealth);  // 只顯示剩餘的愛心
        }
    }
}
