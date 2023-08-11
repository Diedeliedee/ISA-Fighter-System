using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeri.Tools;
using Joeri.Tools.Structure.StateMachine.Advanced;
using UnityEngine;

public partial class PunchingBag
{
    public class Hitstun : CompositeState<PunchingBag>
    {
        private ShakeInstancer m_shake  = null;
        private float m_velocity        = 0f;

        private int m_frames        = 0;
        private int m_framesActive  = 0;

        public void Initiate(int damage, int frames, float knockback)
        {
            m_frames = frames;                              //  Register how many frames of stun.

            m_velocity  = knockback / frames;               //  Register the amount of movement from the knockback.
            m_shake     = new ShakeInstancer                //  Initiate the shake class.
            (
                source.m_model.localPosition,
                (float)damage / source.m_health.maxHealth,
                Mathf.Infinity,
                frames
            );
        }

        public override void OnEnter()
        {
            source.m_animator.enabled = false;  //  Disable the animator, so the shake isn't overidden.
        }

        public override void OnTick()
        {
            //  Switch back to idle if the hitstun is over.
            if (m_framesActive >= m_frames)
            {
                Switch(typeof(Idle));
                return;
            }

            source.xPosition                += m_velocity;              //  Apply knockback.
            source.m_model.localPosition    = m_shake.GetPosition(1f);  //  Apply shake effect.

            m_framesActive++;                                           //  Iterate counter.
        }

        public override void OnExit()
        {
            source.m_model.localPosition    = m_shake.startPosition;    //  Reset the model's position.
            source.m_animator.enabled       = true;                     //  Re-enable the animator.


            //  Reset variables.
            m_shake = null;
            m_velocity      = 0f;

            m_frames        = 0;
            m_framesActive  = 0;
        }
    }

}
