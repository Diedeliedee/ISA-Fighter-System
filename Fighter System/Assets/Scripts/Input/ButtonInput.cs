using UnityEngine;
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

    public ButtonInput(KeyCode key)
    {
        pressed     = Input.GetKeyDown(key);
        holding     = Input.GetKey(key);
        released    = Input.GetKeyUp(key);
    }

    public override bool Equals(object? obj)
    {
        return obj is ButtonInput other && Equals(other);
    }

    public bool Equals(ButtonInput other)
    {
        //  For now we only want the 'holding' variable to have value in determining equality.
        return holding == other.holding;
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
