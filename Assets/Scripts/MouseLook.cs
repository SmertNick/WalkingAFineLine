using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private bool lockMouse;

    private Transform bodyTransform, headTransform;

    private MouseSettings mouseSettings;
    private Vector2 pitchYaw;
    private float pitch;
    private float yaw;
    
    
    void Start()
    {
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        mouseSettings = GameManager.instance.MouseSettings;

        bodyTransform = GetComponent<Transform>();
        headTransform = GetComponentInChildren<Camera>().transform;
        pitchYaw = Vector2.zero;
    }

    void LateUpdate()
    {
        pitchYaw = GetInput() * mouseSettings.MouseSensitivity * Time.deltaTime;
    
        yaw = pitchYaw.x;

        pitch -= pitchYaw.y;
        pitch = Mathf.Clamp(pitch, -mouseSettings.MaxPitchAngle, mouseSettings.MaxPitchAngle);

        headTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        bodyTransform.Rotate(Vector3.up, yaw);
    }

    private Vector2 GetInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        return new Vector2(mouseX, mouseY);
    }
}
