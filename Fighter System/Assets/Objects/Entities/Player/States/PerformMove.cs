using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    public partial class PerformMove : CompositeState<Player>
    {
        public PerformMove(params CompositeState<Player>[] children) : base(children) { }

        public override void OnTick()
        {
            //  If the end frame has been reached, tell it to the source.
            if (source.m_combat.framesExecuting >= source.m_combat.activeMove.endMark)
            {
                Switch(typeof(FreeRoam));
                return;
            }

            source.m_combat.framesExecuting++;
        }

        public override void OnExit()
        {
            source.m_combat.activeMove      = null;
            source.m_combat.framesExecuting = 0;
        }
    }
}
