using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // 解除滑鼠鎖定
        Cursor.visible = true; // 顯示滑鼠
    }

    public void RestartGame()
    {
        Debug.Log("重新開始遊戲");
        SceneManager.LoadScene("MazeScene"); // 你的主遊戲場景名稱
    }

    public void GoToMainMenu()
    {
        Debug.Log("返回主選單");
        SceneManager.LoadScene("MainMenu");
    }
}
