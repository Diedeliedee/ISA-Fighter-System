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
    public class PerformMove : Module<Player>
    {
        private Timer m_timer = new Timer(1f);

        public PerformMove(Player source) : base(source) { }

        public override State Evaluate()
        {
            //  Do stuff.

            if (!m_timer.ResetOnReach(GameManager.deltaTime))
                return RetrieveState(State.Running);

            source.m_performingMove = false;
            return RetrieveState(State.Succes);
        }
    }
}
