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

    void Start()
    {
        // 初始化
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
        // 玩家按下 R 鍵時，顯示下一張對話框
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
            floatingIndex = (floatingIndex + 1) % floatingImages.Length; // 循環 A~C
            yield return new WaitForSeconds(2f); // 每 2 秒切換一次
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        floatingDialogue.gameObject.SetActive(false); // 隱藏兔子頭上的對話框
        interactText.SetActive(false); // 隱藏 "按下 R" 提示
        mainDialogue.gameObject.SetActive(true); // 顯示畫面中央的對話框
        dialogueIndex = 0;
        mainDialogue.sprite = dialogueImages[dialogueIndex]; // 顯示第一張 D
    }

    void ShowNextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogueImages.Length)
        {
            mainDialogue.sprite = dialogueImages[dialogueIndex]; // 顯示下一張對話
        }
        else
        {
            EndDialogue(); // 對話結束
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        mainDialogue.gameObject.SetActive(false); // 隱藏畫面中央的對話框
        floatingDialogue.gameObject.SetActive(true); // 重新顯示兔子頭上的對話框
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactText.SetActive(true); // 顯示 "按下 R 鍵"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactText.SetActive(false); // 隱藏 "按下 R 鍵"
        }
    }
}
