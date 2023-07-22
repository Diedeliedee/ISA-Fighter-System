using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    public InputHistory history     { get; private set; }
    public InputPackage lastPackage { get; private set; }

    public InputHandler()
    {
        history = new InputHistory();
    }

    public InputPackage GetPackage(Gamepad gamepad)
    {
        var package = new InputPackage(gamepad.leftStick, gamepad.buttonSouth, gamepad.buttonEast);

        SavePackage(package);
        return package;
    }

    public InputPackage GetPackage()
    {
        //  Hardcoded buttons for now. :(
        var package = new InputPackage(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.M, KeyCode.K);

        SavePackage(package);
        return package;
    }

    private void SavePackage(InputPackage package)
    {
        //  If the new package is not different from the last, don't bother saving.
        if (package == lastPackage) return;

        history.Add(package, GameManager.instance.frameCount);
        lastPackage = package;
        GameManager.instance.events.onInputChange.Invoke(package);
    }
}
