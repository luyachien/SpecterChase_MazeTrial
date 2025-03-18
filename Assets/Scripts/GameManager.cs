using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coinCount = 0;
    public TMP_Text coinText;

    public GameObject switchLever; // 拖曳開關物件
    public Animator gateAnimator; // 拖曳柵門的 Animator

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddCoin()
    {
        coinCount++;
        coinText.text = "Coins: " + coinCount;

        if (coinCount >= 20) // 當金幣達到 20 個時，顯示開關
        {
            switchLever.SetActive(true);
        }
    }

    private void ShowSwitch()
    {
        if (switchLever != null)
        {
            switchLever.SetActive(true); // 顯示開關
            Debug.Log("開關已顯示！");
        }
    }

    public void OpenGate()
    {
        if (gateAnimator != null)
        {
            gateAnimator.SetTrigger("Open"); // 觸發開門動畫
            Debug.Log("門已開啟！");
        }
    }

    public void EscapeMaze() // 當玩家成功逃出
    {
        Debug.Log("闖關成功！切換到遊戲結束畫面");
        SceneManager.LoadScene("WinScene"); // 切換場景
    }
}
