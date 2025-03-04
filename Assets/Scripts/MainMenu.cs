using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MazeScene"); // 這裡的 "MazeScene" 之後要改成你的遊戲場景名稱
    }
}
