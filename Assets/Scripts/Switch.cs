using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject door; // 連接的門

    private bool canActivate = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
            Debug.Log("按下 E 來打開門！");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }

    void Update()
    {
        if (canActivate && Input.GetKeyDown(KeyCode.E)) // 玩家在範圍內且按下 E
        {
            Debug.Log("門開啟！");
            door.SetActive(false); // 隱藏門（可改成動畫）
        }
    }
}
