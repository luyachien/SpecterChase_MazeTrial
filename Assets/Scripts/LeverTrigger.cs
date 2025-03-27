using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public GameObject promptText; // UI 提示 "請按下E鍵"

    void Start()
    {
        if (promptText != null)
        {
            promptText.SetActive(false); // 遊戲開始時隱藏提示
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.GetCoinCount() >= 20) // 從 GameManager 取得金幣數量
        {
            promptText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptText.SetActive(false);
        }
    }
}
