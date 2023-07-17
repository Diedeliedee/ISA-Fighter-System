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

    public override bool Equals(object? obj)
    {
        return obj is ButtonInput other && Equals(other);
    }

    public bool Equals(ButtonInput other)
    {
        return
            (
                pressed     == other.pressed &&
                holding     == other.holding &&
                released    == other.released
            );
    }

    public static bool operator ==(ButtonInput lhs, ButtonInput rhs)
    {
        return lhs.Equals(rhs);
    }
    
    public static bool operator !=(ButtonInput lhs, ButtonInput rhs)
    {
        return !lhs.Equals(rhs);
    }
}
