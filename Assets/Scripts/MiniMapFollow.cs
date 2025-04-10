using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player; // 指定玩家
    public Vector3 offset = new Vector3(0, 10, 0); // 攝影機與玩家的相對位置

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPos = player.position + offset;
            transform.position = newPos;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f); // 保持向下
        }
    }
}
