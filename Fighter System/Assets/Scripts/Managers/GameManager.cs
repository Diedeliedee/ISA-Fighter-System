using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Structure;

public class GameManager : Singleton<GameManager>
{
    //   Sub-managers:
    private UIManager m_ui          = null;
    private EventManager m_events   = null;

    //  TODO: find proper place for input handler.
    private InputHandler m_input = null;

    #region Properties
    public EventManager events { get => m_events; }
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;

        instance    = this;
        m_input     = new InputHandler();

        m_ui      = GetComponentInChildren<UIManager>();
        m_events  = GetComponentInChildren<EventManager>();
    }

    private void Start()
    {
        m_ui.Setup();
    }

    private void Update()
    {
        m_input.GetPackage();
    }
}
