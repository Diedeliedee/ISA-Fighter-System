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
        /// <summary>
        /// Constructor for the root state machine. Automatically relays the source instances throughout the composite.
        /// </summary>
        public CompositeFSM(T source, params CompositeState<T>[] states) : base(states)
        {
            foreach (var state in states) state.RelaySource(source);
        }

        /// <summary>
        /// Constructor for any sub-state machines.
        /// </summary>
        public CompositeFSM(params CompositeState<T>[] states) : base(states) { }

        /// <summary>
        /// Recursively starts the state machine composite.
        /// </summary>
        public override void Start()
        {
            base.Start();
            ((CompositeState<T>)m_activeState).Start();
        }

        /// <summary>
        /// Recursively tick the chain of active states withing the state machine.
        /// </summary>
        public override void Tick()
        {
            if (m_activeState == null)
            {
                Debug.LogError("Active state is not yet set. Possibly the Start() function has not been called yet.");
                return;
            }

            ((CompositeState<T>)m_activeState).Tick();
        }

        /// <summary>
        /// Recursively resets the state machine back to a default state.
        /// </summary>
        public void Reset()
        {
            ((CompositeState<T>)m_activeState).SwitchToState(m_startState);
            SwitchToState(m_startState);
        }
    } 
}
