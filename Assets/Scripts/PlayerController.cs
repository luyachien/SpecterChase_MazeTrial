using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HeartManager heartManager;
    private bool isInvincible = false; // 是否處於無敵狀態
    public float invincibilityDuration = 1.0f; // 無敵時間 (1 秒)

    void Start()
    {
        heartManager = FindFirstObjectByType<HeartManager>(); // 獲取 HeartManager
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost") && !isInvincible) // 只有當玩家不在無敵狀態時才扣血
        {
            heartManager.TakeDamage(); // 扣一顆愛心
            StartCoroutine(InvincibilityCoroutine()); // 啟動無敵時間
        }
    }

    // 無敵時間協程
    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // 設為無敵
        yield return new WaitForSeconds(invincibilityDuration); // 等待 1 秒
        isInvincible = false; // 取消無敵
    }
}
