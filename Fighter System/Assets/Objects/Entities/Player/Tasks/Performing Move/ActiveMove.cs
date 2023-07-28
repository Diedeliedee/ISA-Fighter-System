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
    public class ActiveMove : Module<Player>
    {
        public ActiveMove(Player source) : base(source) { }

        public override State Evaluate()
        {
            //  If the framecount has reached the mark.
            if (source.m_combat.framesExecuting >= source.m_combat.activeMove.recoveryMark)
            {
                source.m_combat.hitRegister.Clear();    //  Clear the hurtboxes.
                return RetrieveState(State.Succes);     //  Move on to next node.
            }

            //  Call collision check.
            source.m_combat.hitRegister.CheckForHits();

            //  Keep iterating.
            source.m_combat.framesExecuting++;
            return RetrieveState(State.Running);
        }
    }
}
