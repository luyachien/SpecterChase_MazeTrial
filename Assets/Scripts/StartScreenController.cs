using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image StartImage;
    public string sceneToLoad = "MazeScene"; // 換成你的遊戲場景名稱
    public float fadeDuration = 1.5f;

    private bool isHovered = false;
    private Color originalColor;

    void Start()
    {
        originalColor = StartImage.color;
        StartCoroutine(FadeLoop());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 點擊任意地方
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        StartImage.color = new Color(1f, 1f, 1f, StartImage.color.a); // 變白
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        StartImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, StartImage.color.a); // 還原
    }

    System.Collections.IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeImage(0f, 1f));
            yield return StartCoroutine(FadeImage(1f, 0f));
        }
    }

    System.Collections.IEnumerator FadeImage(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            Color baseColor = isHovered ? Color.white : originalColor;
            StartImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Color finalColor = isHovered ? Color.white : originalColor;
        StartImage.color = new Color(finalColor.r, finalColor.g, finalColor.b, to);
    }
}
