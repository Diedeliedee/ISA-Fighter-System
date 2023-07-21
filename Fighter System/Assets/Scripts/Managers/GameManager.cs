using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Structure;

public class GameManager : Singleton<GameManager>
{
    //  Values:
    public const int frameRate      = 10;
    public static float deltaTime   = 0f;

    //   Sub-managers:
    private EntityManager m_entities    = null;
    private EventManager m_events       = null;
    private UIManager m_ui              = null;

    //  TODO: find proper place for input handler.
    private InputHandler m_input = null;

    #region Properties
    public EventManager events      { get => m_events; }

    public InputPackage latestInput { get => m_input.lastPackage; }
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = frameRate;
        deltaTime                   = 1f / frameRate;

        instance    = this;
        m_input     = new InputHandler();

        m_entities  = GetComponentInChildren<EntityManager>();
        m_events    = GetComponentInChildren<EventManager>();
        m_ui        = GetComponentInChildren<UIManager>();
    }

    private void Start()
    {
        m_entities  .Setup();
        m_ui        .Setup();
    }

    private void Update()
    {
        m_input     .GetPackage();
        m_entities  .Tick(deltaTime);
    }
}
