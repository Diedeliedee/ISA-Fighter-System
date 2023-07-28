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
    public class NoActiveMoveSet : Module<Player>
    {
        public NoActiveMoveSet(Player source) : base(source) { }

        public override State Evaluate()
        {
            if (source.m_combat.executingMove)
                return RetrieveState(State.Failure);

            return RetrieveState(State.Succes);
        }
    }
}
