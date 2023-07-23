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
    /// A task in a sequencer that should break the sequence if it is interrupted by a valid move to perform.
    /// TODO: At the moment this class' task is to break the sequence when a input is valid,
    /// however we also need to break the sequence if a move is ongoing, given the current tree design.
    /// </summary>
    public class CheckForInput : Module<Player>
    {
        public CheckForInput(Player source) : base(source) { }

        public override State Evaluate()
        {
            if (source.m_combat.executingMove)
                return RetrieveState(State.Failure);
            
            if (source.m_combat.ExecuteMoveUponValidInput(GameManager.instance.inputHistory))
                return RetrieveState(State.Failure);

            return RetrieveState(State.Succes);
        }
    }
}
