using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure.BehaviorTree;

public partial class Player : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private MovementBase.Settings  m_movementSettings;
    [SerializeField] private MoveSet                m_moveSet;

    //  Sub-components
    private PlayerController m_movement = null;
    private CombatHandler m_combat      = null;

    private BehaviorTree m_behaviorTree = null;

    //  Dependencies:
    private Animator m_animator = null;

    //  TODO: Switch out with actual 'Move' scriptable object.
    private bool m_performingMove = false;

    public void Setup()
    {
        m_animator = GetComponent<Animator>();

        m_movement  = new PlayerController(gameObject, m_movementSettings);
        m_combat    = new CombatHandler(m_moveSet, m_animator);

        m_behaviorTree = new BehaviorTree
            (
                new Selector
                (
                    new Sequence
                    (
                        new CheckForInput(this),
                        new FreeRoam(this)
                    ),
                    new PerformMove(this)
                )
            );
    }

    public void Tick(float deltaTime)
    {
        m_behaviorTree.Tick();
    }
}
