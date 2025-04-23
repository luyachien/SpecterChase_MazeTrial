using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coinCount = 0;
    public TMP_Text coinText;

    public GameObject switchLever;
    public Animator gateAnimator;

    [Header("音效設定")]
    public AudioClip gateOpenSound; // 開門音效
    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 取得音效元件
    }

    public void AddCoin()
    {
        coinCount++;
        coinText.text = "x " + coinCount;

        if (coinCount >= 20)
        {
            switchLever.SetActive(true);
        }
    }

    private void ShowSwitch()
    {
        if (switchLever != null)
        {
            switchLever.SetActive(true);
            Debug.Log("開關已顯示！");
        }
    }

    public void OpenGate()
    {
        if (gateAnimator != null)
        {
            gateAnimator.SetTrigger("Open"); // 播放開門動畫
            Debug.Log("門已開啟！");
        }
    }
    public void OpenGateAudio()
    {
        // 播放開門音效
        if (audioSource != null && gateOpenSound != null)
        {
            audioSource.PlayOneShot(gateOpenSound, 1.5f); // 1.5f 調整音量
        }
        else
        {
            Debug.LogError("音效播放失敗：audioSource 或 gateOpenSound 為 null！");
        }
    }
    
public void EscapeMaze()
    {
        Debug.Log("闖關成功！切換到遊戲結束畫面");
        SceneManager.LoadScene("WinScene");
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
