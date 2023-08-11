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
    [Space]
    [SerializeField] private AnimationClip m_idleClip;
    [SerializeField] private AnimationClip m_deathClip;
    [SerializeField] private AnimationClip m_spawnClip;

    [Header("Reference:")]
    [SerializeField] private Transform m_model;

    //  Properties:
    private float m_startPosition = 0f;

    //  Components:
    private CompositeFSM<PunchingBag> m_stateMachine = null;

    //  Reference:
    private Collider2D m_collider   = null;
    private Animator m_animator     = null;

    private float xPosition
    { 
        get => transform.position.x;
        set => transform.position = new Vector3(value, transform.position.y, transform.position.z);
    }

    public void Setup()
    {
        m_collider      = GetComponent<Collider2D>();
        m_animator      = GetComponentInChildren<Animator>();

        m_startPosition = transform.localPosition.x;

        m_stateMachine = new CompositeFSM<PunchingBag>
        (
            this,

            new Idle(),
            new Spawning(),
            new Hitstun(),
            new Death()
        );
    }

    public void Tick()
    {
        m_stateMachine.Tick();
    }

    public void OnHit(int damage, int stun, float knockback)
    {
        //  Deplete health.
        m_health.AddHealth(-damage);

        //  Call the event for the UI.
        GameManager.instance.events.onBagHealthChange?.Invoke(m_health.percentage);

        //  Switch to OnDeath() instead, if health is depleted.
        if (m_health.health <= 0)
        {
            OnDeath();
            return;
        }

        //  Switch to the hitstun state.
        m_stateMachine.OnSwitch(typeof(Hitstun));
        ((Hitstun)m_stateMachine.activeState).Initiate(damage, stun, knockback);
    }

    public void OnDeath()
    {
        m_stateMachine.OnSwitch(typeof(Death));
    }

    public void Respawn()
    {
        xPosition = m_startPosition;
        m_health.SetHealth(m_health.maxHealth);
        GameManager.instance.events.onBagHealthChange?.Invoke(m_health.percentage);

        m_stateMachine.OnSwitch(typeof(Spawning));
    }

    public void ToIdle()
    {
        m_stateMachine.OnSwitch(typeof(Idle));
    }
}
