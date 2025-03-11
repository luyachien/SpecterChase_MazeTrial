using UnityEngine;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour
{
    public GameObject heartPrefab;  // 愛心的 Prefab
    public Transform heartContainer; // 放愛心的父物件（可以是空物件）

    public int maxHealth = 5;  // 最大生命值
    private int currentHealth; // 當前生命值

    private List<GameObject> hearts = new List<GameObject>(); // 存放愛心物件

    void Start()
    {
        currentHealth = maxHealth;
        GenerateHearts();
    }

    void GenerateHearts()
    {
        // 清除舊的愛心（避免重新生成時重複）
        foreach (var heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        // 生成新的愛心
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            newHeart.transform.localPosition = new Vector3(i * 1.2f, 0, 0); // 排列愛心
            hearts.Add(newHeart);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (hearts.Count > 0)
        {
            Destroy(hearts[hearts.Count - 1]); // 刪除最後一顆愛心
            hearts.RemoveAt(hearts.Count - 1);
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
            // 這裡可以加入遊戲結束的處理
        }
    }
}
