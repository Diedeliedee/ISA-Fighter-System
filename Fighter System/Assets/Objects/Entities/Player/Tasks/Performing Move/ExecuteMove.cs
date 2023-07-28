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
    public class ExecuteMove : Module<Player>
    {
        public ExecuteMove(Player source) : base(source) { }

        public override State Evaluate()
        {
            //  Execute any and all code prior to executing the move.

            //source.m_animator.Play(source.m_combat.activeMove.animation.name);  //  Start animation.
            return RetrieveState(State.Succes);                                 //  Move on to next node.
        }
    }
}
