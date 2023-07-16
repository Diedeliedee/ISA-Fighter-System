using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class InputPackage
{
    public JoystickInput joystick   { get; private set; }

    public ButtonInput punchButton  { get; private set; }
    public ButtonInput kickButton   { get; private set; }

    public InputPackage()
    {
        joystick    = new JoystickInput(0.1f);

        punchButton = new ButtonInput();
        kickButton  = new ButtonInput();
    }

    public void Set(StickControl stick, ButtonControl punch, ButtonControl kick)
    {
        joystick    .Set(stick);
        punchButton .Set(punch);
        kickButton  .Set(kick);
    }
}
