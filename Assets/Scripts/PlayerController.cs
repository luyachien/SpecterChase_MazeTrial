using UnityEngine;
using UnityEngine.UI;  // 引入 UI 命名空間

public class PlayerController : MonoBehaviour
{
    public int health = 5;  // 玩家生命值
    public Text healthText; // 生命值 UI 文字
    public GameObject gameOverScreen; // 遊戲結束畫面

    private void Start()
    {
        UpdateHealthUI(); // 初始化 UI 顯示
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost")) // 如果碰到的是鬼魂
        {
            health--; // 生命值 -1
            UpdateHealthUI(); // 更新 UI

            if (health <= 0)
            {
                GameOver(); // 生命歸 0，遊戲結束
            }
        }
    }

    void UpdateHealthUI()
    {
        healthText.text = "❤️ x " + health;  // 更新 UI 文字
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);  // 顯示遊戲結束畫面
        Time.timeScale = 0; // 暫停遊戲
    }
}
