using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine
{
    public abstract class ModuleState<Source> : State
    {
        /// <summary>
        /// The class that the state machine is harbored in.
        /// </summary>
        protected Source source { get; private set; }

        public ModuleState(Source source)
        {
            this.source = source;
        }
    }
}