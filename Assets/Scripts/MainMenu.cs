using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MazeScene"); // �o�̪� "MazeScene" ����n�令�A���C�������W��
    }
}
