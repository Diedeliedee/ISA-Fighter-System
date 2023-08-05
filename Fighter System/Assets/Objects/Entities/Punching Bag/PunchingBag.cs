using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;
using Joeri.Tools.Gameify;
using Joeri.Tools.Movement.TwoDee;
using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class PunchingBag : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private Health m_health;

    [Header("Reference:")]
    [SerializeField] private Transform m_model;

    private CompositeFSM<PunchingBag> m_stateMachine = null;

    public void Setup()
    {
        m_stateMachine = new CompositeFSM<PunchingBag>
        (
            this,

            new Idle(),
            new Hitstun()
        );
    }

    public void Tick()
    {
        m_stateMachine.Tick();
    }

    public void OnHit(int damage, int stun, float knockback)
    {
        m_health.AddHealth(-damage);

        m_stateMachine.OnSwitch(typeof(Hitstun));
        ((Hitstun)m_stateMachine.activeState).Initiate(damage, stun, knockback);
    }
}
