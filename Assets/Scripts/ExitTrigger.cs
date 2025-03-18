using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 確保是玩家碰到
        {
            Debug.Log("玩家到達出口！");
            GameManager.instance.EscapeMaze(); // 呼叫切換場景的方法
        }
    }
}
