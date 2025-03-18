using UnityEngine;

public class Switch : MonoBehaviour
{
    public Animator switchAnimator;
    public Animator doorAnimator;
    private bool canActivate = false;

    void Start()
    {
        gameObject.SetActive(false); // 開始時隱藏開關
    }

    void Update()
    {
        if (GameManager.instance.coinCount >= 2) // 改成從 GameManager 取得金幣數量
        {
            gameObject.SetActive(true); // 顯示開關
        }

        if (canActivate && Input.GetKeyDown(KeyCode.E)) // 玩家在範圍內且按下 E
        {
            Debug.Log("門開啟！");
            switchAnimator.SetTrigger("Pull"); // 播放拉霸動畫
            doorAnimator.SetTrigger("Open"); // 觸發動畫
        }
    }

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
}
