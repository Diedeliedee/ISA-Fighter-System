using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Structure.StateMachine
{
    /// <summary>
    /// Class handling a class-based finite state machine system,
    /// </summary>
    public class FSM : IStateMachine
    {
        protected State m_activeState       = null;
        protected System.Type m_startState  = null;

        protected readonly Dictionary<System.Type, State> m_states  = new Dictionary<System.Type, State>();

        public State activeState    { get => m_activeState; }
        public State[] states       { get => m_states.Values.ToArray(); }

        public FSM(params State[] states)
        {
            foreach (var state in states)
            {
                state.Setup(this);
                m_states.Add(state.GetType(), state);
            }
            m_startState = states[0].GetType();
        }

        public virtual void Start()
        {
            SwitchToState(m_startState);
        }

        public virtual void Tick()
        {
            if (m_activeState == null)
            {
                Debug.LogError("Active state is not yet set. Possibly the Start() function has not been called yet.");
                return;
            }

            m_activeState.OnTick();
        }

        public virtual void SwitchToState(System.Type state)
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