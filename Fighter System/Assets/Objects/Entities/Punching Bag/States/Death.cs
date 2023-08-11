using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeri.Tools.Structure.StateMachine.Advanced;
using UnityEngine;

public partial class PunchingBag
{
    public class Death : CompositeState<PunchingBag>
    {
        public override void OnEnter()
        {
            source.m_animator.Play(source.m_deathClip.name);
            source.m_collider.enabled = false;
        }
    }
}
