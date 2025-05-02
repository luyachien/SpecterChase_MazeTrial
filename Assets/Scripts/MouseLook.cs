using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;      // 角色本體，用來左右旋轉
    public Transform playerCamera;    // 相機，用來上下旋轉
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 鎖定滑鼠在畫面中間
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 限制不能仰頭超過後腦勺

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 僅上下看
        playerBody.Rotate(Vector3.up * mouseX); // 左右轉動角色
    }
}
