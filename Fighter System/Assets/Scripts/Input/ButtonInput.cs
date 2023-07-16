using UnityEngine.InputSystem.Controls;

public struct ButtonInput
{
    public readonly bool pressed;
    public readonly bool holding;
    public readonly bool released;

    public ButtonInput(ButtonControl button)
    {
        pressed     = button.wasPressedThisFrame;
        holding     = button.isPressed;
        released    = button.wasReleasedThisFrame;
    }
}
