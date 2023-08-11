using System;

public class EventManager
{
    public Action<InputPackage> onInputChange { get; set; }

    public Action<HitInstance> onEntityHit  { get; set; }
    public Action<float> onBagHealthChange  { get; set; }
}
