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
    public const int frameRate      = 60;
    public static float deltaTime   = 0f;

    //   Sub-managers:
    private EntityManager m_entities    = null;
    private UIManager m_ui              = null;
    private CameraManager m_camera      = null;
    private ParticleManager m_particles = null;

    private TimeManager m_time      = new TimeManager();
    private EventManager m_events   = new EventManager();

    //  TODO: find proper place for input handler.
    private InputHandler m_input = new InputHandler();

    #region Properties
    public EventManager events { get => m_events; }

    public InputHistory inputHistory    { get => m_input.history; }
    public InputPackage latestInput     { get => m_input.lastPackage; }

    public uint frameCount { get => m_time.frameCount; }
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = frameRate;
        deltaTime                   = 1f / frameRate;

        instance    = this;

        m_entities  = GetComponentInChildren<EntityManager>();
        m_ui        = GetComponentInChildren<UIManager>();
        m_camera    = GetComponentInChildren<CameraManager>();
        m_particles = GetComponentInChildren<ParticleManager>();
    }

    private void Start()
    {
        m_entities  .Setup();
        m_time      .Setup();
        m_ui        .Setup();
        m_camera    .Setup();
        m_particles .Setup();
    }

    private void Update()
    {
        if (!m_time.paused)
        {
            m_input     .GetPackage();
            m_entities  .Tick();
            m_camera    .Tick();
        }

        m_time.Tick();
    }
}
