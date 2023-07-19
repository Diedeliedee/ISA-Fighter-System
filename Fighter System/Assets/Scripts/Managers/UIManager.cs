using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InputLogger m_inputLogger = null;

    public void Setup()
    {
        m_inputLogger = GetComponentInChildren<InputLogger>();

        m_inputLogger.Setup();
    }
}
