using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure.BehaviorTree;

public partial class Player
{
    public class FreeRoam : Module<Player>
    {
        public FreeRoam(Player source) : base(source) { }

        public override State Evaluate()
        {
            var movementInput = GameManager.instance.latestInput.joystick.vector;

            source.m_movement.ApplyInput(movementInput, GameManager.deltaTime);
            return State.Succes;
        }
    }
}
