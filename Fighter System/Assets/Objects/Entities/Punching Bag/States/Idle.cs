using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeri.Tools.Structure.StateMachine.Advanced;
using UnityEngine;

public partial class PunchingBag
{
    public class Idle : CompositeState<PunchingBag>
    {
        private const int m_recoveryDuration    = 10;
        private float m_velocity                = 0f;

        public override void OnEnter()
        {
            source.m_animator.Play(source.m_idleClip.name);
        }

        public override void OnTick()
        {
            var currentPos  = source.transform.localPosition;
            var targetPos   = new Vector3(source.m_startPosition, currentPos.y, currentPos.z);

            currentPos.x                    = Mathf.SmoothDamp(currentPos.x, targetPos.x, ref m_velocity, m_recoveryDuration, Mathf.Infinity, 1f);
            source.transform.localPosition  = currentPos;
        }
    }
}
