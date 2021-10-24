using UnityEngine;

[CreateAssetMenu(fileName = "KeyBindings", menuName = "WalkingAFineLine/KeyBindings")]
public class KeyBindings : ScriptableObject
{
    [Header("KeyBinds")]
    [SerializeField] private KeyCode forward = KeyCode.W;
    [SerializeField] private KeyCode backward = KeyCode.S;
    [SerializeField] private KeyCode strafeLeft = KeyCode.A;
    [SerializeField] private KeyCode strafeRight = KeyCode.D;
    [SerializeField] private KeyCode jump = KeyCode.Space;
    [SerializeField] private KeyCode fire = KeyCode.Mouse0;

    public KeyCode Forward => forward;
    public KeyCode Backward => backward;
    public KeyCode StrafeLeft => strafeLeft;
    public KeyCode StrafeRight => strafeRight;
    public KeyCode Jump => jump;
    public KeyCode Fire => fire;
}
