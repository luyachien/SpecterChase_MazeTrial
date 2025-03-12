using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HeartManager heartManager; // 生命值管理器

    void Start()
    {
        heartManager = FindFirstObjectByType<HeartManager>();
        if (heartManager == null)
        {
            Debug.LogError("HeartManager 未找到！");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player 碰到了：" + other.gameObject.name); // 測試碰撞

        if (other.CompareTag("Ghost"))
        {
            Debug.Log("碰到了鬼魂！");

            if (heartManager != null)
            {
                heartManager.TakeDamage();
                Debug.Log("成功扣血！");
            }
            else
            {
                Debug.LogError("HeartManager 未找到！");
            }
        }
    }

    void Update()
    {
        int ghostLayerMask = LayerMask.GetMask("Ghost"); // 確保 Ghost 層級存在
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f, ghostLayerMask);

        if (hitColliders.Length > 0)
        {
            foreach (Collider hit in hitColliders)
            {
                Debug.Log("偵測到鬼魂：" + hit.gameObject.name);
            }
        }
        else
        {
            Debug.Log("沒有偵測到任何鬼魂！");
        }
    }


}
