using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class CompositeFSM<T> : FSM
    {
        public readonly T source;

        public CompositeFSM(T source, params CompositeState<T>[] states)
        {
            this.source = source;
            Configure(states);
        }

        public override void Tick()
        {
            ((CompositeState<T>)m_activeState).Tick();
        }

        /// <summary>
        /// Resets the state machine back to a default state.
        /// </summary>
        public void Reset()
        {
            SwitchToState(m_startState);
        }
    } 
}
