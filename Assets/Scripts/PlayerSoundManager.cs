using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("音效設定")]
    public AudioClip walkSound; // 走路音效
    public AudioClip runSound;  // 奔跑音效
    public AudioSource audioSource; // 音效播放器

    private CharacterController characterController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 取得 AudioSource
        characterController = GetComponent<CharacterController>(); // 取得角色控制器

        if (audioSource == null)
        {
            Debug.LogError("PlayerSoundManager 缺少 AudioSource！");
        }
    }

    void Update()
    {
        HandleMovementSound();
    }

    private void HandleMovementSound()
    {
        if (characterController.isGrounded) // 玩家站在地面上
        {
            if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))))
            {
                PlaySound(runSound);
            }
            else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) // 走路
            {
                PlaySound(walkSound);
            }
            else // 玩家沒有移動，停止音效
            {
                StopSound();
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip, 1.0f); // 播放音效
        }
    }

    private void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
