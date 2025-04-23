using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioClip coinSound; // 設定金幣音效
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 取得 AudioSource 元件
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coinSound != null && audioSource != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position, 2.5f);
            }

            GameManager.instance.AddCoin(); // 增加金幣數量
            Destroy(gameObject); // 刪除金幣
        }
    }
}
