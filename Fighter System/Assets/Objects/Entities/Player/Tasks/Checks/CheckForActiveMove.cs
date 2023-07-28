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
    /// <summary>
    /// A task in a sequencer that should break the sequence if the player is already executing a move.
    /// </summary>
    public class CheckForActiveMove : Module<Player>
    {
        public CheckForActiveMove(Player source) : base(source) { }

        public override State Evaluate()
        {
            if (source.m_combat.executingMove)
                return RetrieveState(State.Failure);

            return RetrieveState(State.Succes);
        }
    }
}
