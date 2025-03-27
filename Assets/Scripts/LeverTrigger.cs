using UnityEngine;
using UnityEngine.UI; // 如果 UI 文字是 TextMeshPro，請改為 using TMPro;
using TMPro;

public class LeverTrigger : MonoBehaviour
{
    public GameObject promptText; // UI 提示物件，例如 "請按下E鍵"

    void Start()
    {
        if (promptText != null)
        {
            promptText.SetActive(false); // 遊戲開始時隱藏提示
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 玩家進入範圍
        {
            promptText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 玩家離開範圍
        {
            promptText.SetActive(false);
        }
    }
}
