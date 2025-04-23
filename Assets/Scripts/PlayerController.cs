using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HeartManager heartManager;
    private bool isInvincible = false; // 是否處於無敵狀態
    public float invincibilityDuration = 0.25f; // 無敵時間 (0.5秒)

    [Header("音效設定")]
    public AudioClip DamageSound;
    public AudioSource audioSource;

    void Start()
    {
        heartManager = FindFirstObjectByType<HeartManager>(); // 獲取 HeartManager
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost") && !isInvincible) // 只有當玩家不在無敵狀態時才扣血
        {
            if (audioSource != null && DamageSound != null)
            {
                audioSource.PlayOneShot(DamageSound, 1.5f); // 1.5f 調整音量
            }
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
