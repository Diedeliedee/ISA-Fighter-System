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
    public class CheckForInput : Module<Player>
    {
        public CheckForInput(Player source) : base(source) { }

        public override State Evaluate()
        {
            //  If an executable move has been found..
            if (source.m_combat.CheckForValidInput(GameManager.instance.inputHistory, out MoveConcept move))
            {
                source.m_combat.ExecuteMove(move);  //  Activate the move in the combat handler.
                return RetrieveState(State.Succes); //  Return succes.
            }

            //  Otherwise, return failure.
            return RetrieveState(State.Failure);
        }
    }
}
