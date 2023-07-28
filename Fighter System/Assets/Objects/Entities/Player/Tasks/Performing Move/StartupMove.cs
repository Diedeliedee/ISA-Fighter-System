using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;
using Joeri.Tools.Structure.BehaviorTree;

public partial class Player
{
    public class StartupMove : Module<Player>
    {
        public StartupMove(Player source) : base(source) { }

        public override State Evaluate()
        {
            //  If the framecount has reached the mark.
            if (source.m_combat.framesExecuting >= source.m_combat.activeMove.activeMark)
            {
                source.m_combat.hitRegister.hurtboxes = source.m_combat.activeMove.hurtboxes;   //  Enable hurtboxes.
                return RetrieveState(State.Succes);                                             //  Move on to next node.
            }

            //  Otherwise, keep iterating.
            source.m_combat.framesExecuting++;
            return RetrieveState(State.Running);
        }
    }
}
