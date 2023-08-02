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
    public class FreeRoam : CompositeState<Player>
    {
        public override void OnEnter()
        {
            source.m_animator.Play(source.m_idleAnimation.name);
        }

        public override void OnTick()
        {
            //  Cache input.
            var movementInput = GameManager.instance.latestInput.joystick.vector.x;

            //  Check if a valid input has been detected.
            if (source.m_combat.CheckForValidInput(GameManager.instance.inputHistory, out MoveConcept move))
            {
                source.m_combat.ExecuteMove(move);
                SwitchToState(typeof(PerformMove));
            }

            //  Apply movement.
            source.m_movement.ApplyInput(movementInput, GameManager.deltaTime);
        }
    }
}
