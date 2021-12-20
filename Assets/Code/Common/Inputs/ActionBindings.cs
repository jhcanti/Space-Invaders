using UnityEngine;

[CreateAssetMenu(fileName = "bindings", menuName = "ControlBinding")]
public class ActionBindings : ScriptableObject
{
    [Header("Movement")]
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public KeyCode fire;
    public KeyCode pause;
}
