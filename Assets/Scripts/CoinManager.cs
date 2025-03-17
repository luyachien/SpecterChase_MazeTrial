using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int totalCoinsNeeded = 20; // 需要多少金幣才會解鎖開關
    private int currentCoins = 0;

    public GameObject switchObject; // 開關物件
    public Text coinText; // 顯示金幣數量的 UI（如果有的話）

    void Start()
    {
        switchObject.SetActive(false); // 一開始隱藏開關
        UpdateCoinUI();
    }

    public void CollectCoin()
    {
        currentCoins++;
        UpdateCoinUI();

        if (currentCoins >= totalCoinsNeeded)
        {
            switchObject.SetActive(true); // 顯示開關
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + currentCoins;
        }
    }
}
