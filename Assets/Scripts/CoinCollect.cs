using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddCoin(); // 增加金幣數量
            Destroy(gameObject); // 刪除金幣
        }
    }
}
