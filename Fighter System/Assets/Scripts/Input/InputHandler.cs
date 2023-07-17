using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    public LinkedList<InputPackage> history { get; private set; }
    public InputPackage lastPackage         { get; private set; }

    public InputHandler()
    {
        history = new LinkedList<InputPackage>();
    }

    public InputPackage GetPackage(Gamepad gamepad)
    {
        var package = new InputPackage(gamepad.leftStick, gamepad.buttonSouth, gamepad.buttonEast);

        history.AddFirst(package);

        lastPackage = package;
        return        package;
    }
}
