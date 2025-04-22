using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("點擊音效")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    private bool isClicked = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        if (!isClicked)
        {
            isClicked = true;
            StartCoroutine(PlayClickAndLoadScene("MazeScene"));
        }
    }

    public void GoToMainMenu()
    {
        if (!isClicked)
        {
            isClicked = true;
            StartCoroutine(PlayClickAndLoadScene("MainMenu"));
        }
    }

    private IEnumerator PlayClickAndLoadScene(string sceneName)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            yield return new WaitForSeconds(clickSound.length);
        }

        SceneManager.LoadScene(sceneName);
    }
}
