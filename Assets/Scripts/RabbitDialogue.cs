using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RabbitDialogue : MonoBehaviour
{
    [Header("兔子頭上的對話框 (循環播放 A~C)")]
    public Image floatingDialogue;  // 兔子頭上顯示的對話框
    public Sprite[] floatingImages; // 循環顯示 A~C 的圖片
    private int floatingIndex = 0;

    [Header("畫面中央的對話框 (按 R 播放 D~F)")]
    public Image mainDialogue;      // 畫面中間顯示的對話框
    public Sprite[] dialogueImages; // 按 R 切換 D~F 的圖片
    private int dialogueIndex = 0;

    [Header("玩家偵測範圍")]
    public GameObject interactText; // "按下 R 鍵" 提示文字
    private bool isPlayerNear = false;
    private bool isDialogueActive = false;

    [Header("對話音效")]
    public AudioSource audioSource;       // 用來播放音效的 AudioSource
    public AudioClip dialogueClip;        // 對話音效

    void Start()
    {
        if (floatingDialogue != null && floatingImages.Length > 0)
        {
            StartCoroutine(LoopFloatingDialogue());
        }
        if (mainDialogue != null)
        {
            mainDialogue.gameObject.SetActive(false); // 隱藏畫面中的對話框
        }
        if (interactText != null)
        {
            interactText.SetActive(false); // 隱藏 "按下 R" 提示
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.R))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                ShowNextDialogue();
            }
        }
    }

    IEnumerator LoopFloatingDialogue()
    {
        while (true)
        {
            floatingDialogue.sprite = floatingImages[floatingIndex]; // 切換對話框圖片
            floatingIndex = (floatingIndex + 1) % floatingImages.Length;
            yield return new WaitForSeconds(2f);
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        floatingDialogue.gameObject.SetActive(false);
        interactText.SetActive(false);
        mainDialogue.gameObject.SetActive(true);
        dialogueIndex = 0;
        mainDialogue.sprite = dialogueImages[dialogueIndex];

        PlayDialogueSound(); // 播放對話音效
    }

    void ShowNextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogueImages.Length)
        {
            mainDialogue.sprite = dialogueImages[dialogueIndex];
            PlayDialogueSound(); // 播放對話音效
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        mainDialogue.gameObject.SetActive(false);
        floatingDialogue.gameObject.SetActive(true);
    }

    void PlayDialogueSound()
    {
        if (audioSource != null && dialogueClip != null)
        {
            audioSource.PlayOneShot(dialogueClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactText.SetActive(false);
        }
    }
}
