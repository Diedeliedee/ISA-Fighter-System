using UnityEngine.InputSystem.Controls;

public class ButtonInput
{
    public bool pressed     { get; private set; }
    public bool holding     { get; private set; }
    public bool released    { get; private set; }

    public void Set(ButtonControl button)
    {
        pressed     = button.wasPressedThisFrame;
        holding     = button.isPressed;
        released    = button.wasReleasedThisFrame;
    }
}
