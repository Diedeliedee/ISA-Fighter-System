using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Structure.StateMachine
{
    /// <summary>
    /// Class handling a class-based finite state machine system,
    /// </summary>
    public class FSM : IStateMachine
    {
        protected State m_activeState                               = null;
        protected readonly Dictionary<System.Type, State> m_states  = new Dictionary<System.Type, State>();

        public System.Type activeState { get => m_activeState.GetType(); }

        public FSM(params State[] states)
        {
            foreach (var state in states)
            {
                state.Setup(this);
                m_states.Add(state.GetType(), state);
            }
            SwitchToState(states[0].GetType());
        }

        public virtual void Tick()
        {
            m_activeState.OnTick();
        }

        public void SwitchToState(System.Type state)
        {
            m_activeState?.OnExit();
            try     { m_activeState = m_states[state]; }
            catch   { Debug.LogError($"The state: '{state.Name}' is not found within the state dictionary."); return; }
            m_activeState?.OnEnter();
        }

        /// <summary>
        /// Tells the state machine to switch to another state of the passed in generic type.
        /// </summary>
        public T SwitchToState<T>() where T : State
        {
            SwitchToState(typeof(T));
            return (T)m_activeState;
        }

        /// <summary>
        /// Function to call the gizmos of the current active state.
        /// </summary>
        public virtual void DrawGizmos(Vector3 position)
        {
            void DrawLabel(string label)
            {
                GizmoTools.DrawLabel(position, label, Color.black);
            }

            if (m_activeState == null) return;
            //  Drawing text in the world describing the current state the agent is in.
            DrawLabel(m_activeState.GetType().Name);

            //  Drawing the gizmos of the current state, if it isn't null.
            m_activeState.OnDrawGizmos();
        }
    }
}