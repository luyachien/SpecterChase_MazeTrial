using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;


public class StartScreenController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image StartImage;
    public string sceneToLoad = "MazeScene";
    public float fadeDuration = 1.5f;

    [Header("點擊音效")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    private bool isHovered = false;
    private bool isClicked = false; // 防止重複點擊
    private Color originalColor;

    void Start()
    {
        originalColor = StartImage.color;
        StartCoroutine(FadeLoop());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;
            StartCoroutine(PlayClickAndLoadScene());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        StartImage.color = new Color(1f, 1f, 1f, StartImage.color.a);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        StartImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, StartImage.color.a);
    }

    System.Collections.IEnumerator FadeLoop()
    {
        while (!isClicked)
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

    IEnumerator PlayClickAndLoadScene()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            yield return new WaitForSeconds(clickSound.length); // 等音效播完
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
