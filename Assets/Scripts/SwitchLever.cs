using UnityEngine;

public class SwitchLever : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.OpenGate(); // 呼叫 GameManager 來開門
            Destroy(gameObject); // 刪除開關（不讓玩家重複觸發）
        }
    }
}
