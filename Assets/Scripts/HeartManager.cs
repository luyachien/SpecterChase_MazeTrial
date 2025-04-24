using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;  // 愛心的 Prefab (PNG 圖片)
    public Transform heartContainer;  // 愛心 UI 的父物件
    public int maxHealth = 5;  // 最大生命值
    private int currentHealth;
    private GameObject[] hearts;  // 存放所有愛心的陣列
    
    [Header("音效設定")]
    public AudioClip GameOverSound;
    public AudioSource audioSource;
    public GameObject bgmManager;

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

        // 當生命值歸零時，遊戲結束
        if (currentHealth <= 0)
        {
            GameOver();
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

    // 遊戲結束處理
    void GameOver()
    {
        if (audioSource != null && GameOverSound != null)
        {
            StartCoroutine(PlayGameOverSoundAndEscape()); // 啟動協程
        }
    }

    private IEnumerator PlayGameOverSoundAndEscape()
    {
        // 停止背景音樂
        if (bgmManager != null)
        {
            AudioSource bgmSource = bgmManager.GetComponent<AudioSource>();
            if (bgmSource != null)
            {
                bgmSource.Stop();
                Debug.Log("背景音樂已停止！");
            }
        }

        audioSource.PlayOneShot(GameOverSound, 1.5f); // 播放音效
        yield return new WaitForSeconds(GameOverSound.length); // 等待音效播放完畢
        Debug.Log("遊戲結束！");
        SceneManager.LoadScene("GameOverScene"); // 切換到 Game Over 畫面
    }
}
