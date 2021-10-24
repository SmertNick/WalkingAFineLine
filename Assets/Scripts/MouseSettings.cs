using UnityEngine;

[CreateAssetMenu(fileName = "MouseSettings", menuName = "WalkingAFineLine/MouseSettings")]
public class MouseSettings : ScriptableObject
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float maxPitchAngle = 85f;
    
    public float MouseSensitivity => mouseSensitivity;
    public float MaxPitchAngle => maxPitchAngle;

}
