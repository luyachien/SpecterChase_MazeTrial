using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject floatingText; // 兔子頭上的小對話框
    public GameObject dialogueBox; // 畫面中央的大對話框
    public Text floatingTextUI;
    public Text dialogueText;
    public Text interactionText; // "按下 R 鍵" 提示
    public List<string> dialogues; // 兔子的對話內容

    private int dialogueIndex = 0;
    private bool isPlayerNear = false;
    private bool isTalking = false;

    void Start()
    {
        StartCoroutine(FloatingTextLoop());
        dialogueBox.SetActive(false);
        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.R))
        {
            if (!isTalking)
            {
                StartDialogue();
            }
            else
            {
                NextDialogue();
            }
        }
    }

    IEnumerator FloatingTextLoop()
    {
        while (true)
        {
            floatingTextUI.text = "請靠近我！";
            yield return new WaitForSeconds(1.5f);
            floatingTextUI.text = "我會告訴你玩法！";
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void StartDialogue()
    {
        isTalking = true;
        floatingText.SetActive(false);
        dialogueBox.SetActive(true);
        dialogueIndex = 0;
        dialogueText.text = dialogues[dialogueIndex];
    }

    private void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isTalking = false;
        dialogueBox.SetActive(false);
        floatingText.SetActive(true);
        StartCoroutine(FloatingTextLoop());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            floatingText.SetActive(false);
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionText.gameObject.SetActive(false);
            if (!isTalking)
            {
                floatingText.SetActive(true);
                StartCoroutine(FloatingTextLoop());
            }
        }
    }
}
