using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HealthManager healthManager;

    void Start()
    {
        healthManager = FindFirstObjectByType<HealthManager>(); // 取得場景中第一個找到的 HealthManager
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost")) // 如果碰到鬼魂
        {
            healthManager.TakeDamage(1);
        }
    }
}
