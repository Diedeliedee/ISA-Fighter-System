using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public struct InputPackage
{
    public readonly JoystickInput   joystick;
    public readonly ButtonInput     punchButton;
    public readonly ButtonInput     kickButton;

    public InputPackage(StickControl stick, ButtonControl punch, ButtonControl kick)
    {
        joystick    = new JoystickInput(stick);
        punchButton = new ButtonInput(punch);
        kickButton  = new ButtonInput(kick);
    }

    public InputPackage(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode punch, KeyCode kick)
    {
        joystick    = new JoystickInput(up, down, left, right);
        punchButton = new ButtonInput(punch);
        kickButton  = new ButtonInput(kick);
    }

    public override bool Equals(object? obj)
    {
        return obj is InputPackage other && Equals(other);
    }

    public bool Equals(InputPackage other)
    {
        return
            (
                joystick    == other.joystick &&
                punchButton == other.punchButton &&
                kickButton  == other.kickButton
            );
    }

    public static bool operator ==(InputPackage lhs, InputPackage rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(InputPackage lhs, InputPackage rhs)
    {
        return !lhs.Equals(rhs);
    }
}
