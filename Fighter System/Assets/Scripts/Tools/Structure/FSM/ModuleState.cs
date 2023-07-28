using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine
{
    /// <summary>
    /// Abstract base for a state within a state machine.
    /// </summary>
    public abstract class ModuleState<Source> : State
    {
        /// <summary>
        /// The settings of the state, as an abstract settings interface.
        /// </summary>
        private Settings settings { get; set; }

        /// <summary>
        /// The root class that the state machine is harbored in.
        /// </summary>
        protected Source source { get; private set; }

        /// <summary>
        /// Create a new state, and pass in the state's settings.
        /// </summary>
        public ModuleState(Source source, Settings settings)
        {
            this.source     = source;
            this.settings   = settings;
        }

        /// <summary>
        /// Create a new state, without any required settings.
        /// </summary>
        public ModuleState(Source source)
        {
            this.source = source;
            settings    = null;
        }

        /// <summary>
        /// Switches to another state using a generic variable.
        /// </summary>
        protected T SwitchToState<T>() where T : ModuleState<Source>
        {
            return machine.SwitchToState<T>();
        }

        /// <returns>The state's settings class casted as a settings sub-class.</returns>
        protected T GetSettings<T>() where T : Settings
        {
            return settings as T;
        }

        public abstract class Settings { }
    }
}