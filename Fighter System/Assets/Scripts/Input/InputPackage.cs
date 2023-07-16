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
}
