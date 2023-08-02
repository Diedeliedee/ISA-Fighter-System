using System;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine
{
    public interface IStateMachine
    {
        /// <summary>
        /// Starts the state machine by switching to the default state.
        /// </summary>
        public void Start();

        /// <summary>
        /// Calls the Tick() function within the active state.
        /// </summary>
        public void Tick();

        /// <summary>
        /// Tells the state machine to switch to another state of the passed in type parameter.
        /// </summary>
        public void SwitchToState(Type state);
    }
}
