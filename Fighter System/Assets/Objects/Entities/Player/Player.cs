using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Movement.TwoDee;
using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player : MonoBehaviour
{
    [Header("Settings:")]
    [SerializeField] private TwoDeeMovement.Settings    m_movementSettings;
    [SerializeField] private HitRegister<PunchingBag>   m_hitRegister;
    [SerializeField] private AnimationClip              m_idleAnimation;
    [Space]
    [SerializeField] private MoveSet                    m_moveSet;

    //  Sub-components
    private TwoDeeMovement m_movement   = null;
    private CombatHandler m_combat      = null;

    private CompositeFSM<Player> m_stateMachine = null;

    //  Dependencies:
    private Animator m_animator = null;

    public void Setup()
    {
        m_animator = GetComponentInChildren<Animator>();

        m_movement  = new TwoDeeMovement(transform, m_movementSettings);
        m_combat    = new CombatHandler(m_moveSet, m_hitRegister, m_animator);

        m_stateMachine = new CompositeFSM<Player>
        (
            this,

            new FreeRoam(),
            new PerformMove
            (
                new Startup(),
                new Active(),
                new Recovery()
            )
        );
    }

    public void Tick(float deltaTime)
    {
        m_stateMachine.Tick();
    }

    public void OnDrawGizmos()
    {
        m_hitRegister.Draw(transform.position);
    }
}
