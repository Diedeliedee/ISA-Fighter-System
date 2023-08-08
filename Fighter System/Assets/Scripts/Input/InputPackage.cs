using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct InputPackage
{
    public readonly JoystickInput   joystick;
    public readonly ButtonInput     punchButton;
    public readonly ButtonInput     kickButton;

    public InputPackage(JoystickInput joystick, ButtonInput punchButton, ButtonInput kickButton)
    {
        this.joystick       = joystick;
        this.punchButton    = punchButton;
        this.kickButton     = kickButton;
    }

    public InputPackage(Gamepad gamepad)
    {
        joystick    = new JoystickInput(gamepad.leftStick) + new JoystickInput(gamepad.dpad);
        punchButton = new ButtonInput(gamepad.buttonSouth);
        kickButton  = new ButtonInput(gamepad.buttonEast);
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

    public static InputPackage operator + (InputPackage lhs, InputPackage rhs)
    {
        return new InputPackage(lhs.joystick + rhs.joystick, lhs.punchButton + rhs.punchButton, lhs.kickButton + rhs.kickButton);
    }
}
