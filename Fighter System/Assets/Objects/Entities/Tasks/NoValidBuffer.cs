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
    public class NoValidBuffer : Module<Player>
    {
        public NoValidBuffer(Player source) : base(source) { }

        public override State Evaluate()
        {
            if (GameManager.instance.latestInput.punchButton.holding)
            {
                source.m_performingMove = true;
                return RetrieveState(State.Failure);
            }
            if (source.m_performingMove)
            {
                return RetrieveState(State.Failure);
            }
            return RetrieveState(State.Succes);
        }
    }
}
