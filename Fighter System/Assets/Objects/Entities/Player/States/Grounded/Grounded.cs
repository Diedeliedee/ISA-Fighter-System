using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    public class Grounded : CompositeState<Player>
    {
        public Grounded(params CompositeState<Player>[] children) : base(children) { }

        public override void OnTick()
        {
            //  Cache input.
            var movementInput = GameManager.instance.latestInput.joystick.vector.x;

            //  Check if a valid input has been detected.
            if (source.m_combat.CheckForValidInput(GameManager.instance.inputHistory, out MoveConcept move))
            {
                source.m_combat.ExecuteMove(move);
                Switch(typeof(PerformMove));
                return;
            }

            //  Apply movement.
            source.m_movement.ApplyInput(movementInput, GameManager.deltaTime);
        }
    }
}
