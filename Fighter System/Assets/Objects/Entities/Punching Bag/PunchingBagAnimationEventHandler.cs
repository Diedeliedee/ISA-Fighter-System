using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class PunchingBagAnimationEventHandler : MonoBehaviour
{
    public UnityEvent onSpawnEnd;
    public UnityEvent onDeathEnd;

    public void OnSpawnEnd() => onSpawnEnd.Invoke();
    public void OnDeathEnd() => onDeathEnd.Invoke();
}
