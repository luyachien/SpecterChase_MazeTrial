using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int totalCoins = 0;
    public int requiredCoins = 20;
    public GameObject switchLever;  // 拉霸開關

    void Start()
    {
        switchLever.SetActive(false);  // 一開始隱藏開關
    }

    public void CollectCoin()
    {
        totalCoins++;
        if (totalCoins >= requiredCoins)
        {
            switchLever.SetActive(true);  // 顯示開關
        }
    }
}
