using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InputLogger m_inputLogger   = null;
    private HealthBar m_healthBar       = null;

    public void Setup()
    {
        m_inputLogger   = GetComponentInChildren<InputLogger>();
        m_healthBar     = GetComponentInChildren<HealthBar>();

        m_inputLogger   .Setup();
        m_healthBar     .Setup();
    }

    public void Tick()
    {
        m_healthBar.Tick();
    }
}
