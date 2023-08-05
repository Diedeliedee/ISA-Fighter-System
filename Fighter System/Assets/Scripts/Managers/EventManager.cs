using System;

public class EventManager
{
    public Action<InputPackage> onInputChange;

    public Action<int> onEntityHit;
}
