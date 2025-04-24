using UnityEngine;
using System.Collections;

public class ExitTrigger : MonoBehaviour
{
    [Header("音效設定")]
    public AudioClip WinSound;
    public AudioSource audioSource;
    public GameObject bgmManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 確保是玩家碰到
        {
            if (audioSource != null && WinSound != null)
            {
                StartCoroutine(PlayWinSoundAndEscape()); // 啟動協程
            }
            else
            {
                Debug.LogError("音效播放失敗：audioSource 或 WinSound 為 null！");
            }
        }
    }

    private IEnumerator PlayWinSoundAndEscape()
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

        audioSource.PlayOneShot(WinSound, 1.5f); // 播放音效
        yield return new WaitForSeconds(WinSound.length); // 等待音效播放完畢
        Debug.Log("玩家到達出口！");
        GameManager.instance.EscapeMaze(); // 呼叫切換場景的方法
    }
}
