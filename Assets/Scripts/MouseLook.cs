using UnityEngine;
using UnityEngine.Serialization;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private MouseSettings mouseSettings;
    [SerializeField] private Transform playerBody;
    
    private float pitch = 0f; 
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSettings.MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSettings.MouseSensitivity * Time.deltaTime;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -mouseSettings.MaxPitchAngle, mouseSettings.MaxPitchAngle);

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        playerBody.Rotate(Vector3.up, mouseX);
    }
}
