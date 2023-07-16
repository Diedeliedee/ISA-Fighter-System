using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    public InputPackage lastPackage { get; private set; }

    public InputPackage GetPackage(Gamepad gamepad)
    {
        var package = new InputPackage(gamepad.leftStick, gamepad.buttonSouth, gamepad.buttonEast);

        lastPackage = package;
        return package;
    }
}
