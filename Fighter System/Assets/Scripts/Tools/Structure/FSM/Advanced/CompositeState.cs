using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;
using System;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public abstract class CompositeState<T> : State
    {
        private CompositeState<T> m_parent      = null;
        private CompositeFSM<T> m_stateMachine  = null;

        public CompositeState<T> parent
        {
            get
            {
                if (m_parent == null)
                    Debug.LogError($"State: {this} does not have a parent state.");
                return m_parent;
            }
        }
        public CompositeState<T>[] children
        {
            get
            {
                if (m_stateMachine == null || Util.IsUnusableArray(m_stateMachine.states))
                    Debug.LogError($"State: {this} does not have any child states.");
                return m_stateMachine.states as CompositeState<T>[];
            }
        }

        protected new CompositeFSM<T> owner { get => base.owner as CompositeFSM<T>; }
        protected T source                  { get => owner.source; }

        public CompositeState(params CompositeState<T>[] children)
        {
            m_stateMachine = new CompositeFSM<T>(source, children);
            foreach (var child in children) child.AttachParent(this);
        }

        public override void SwitchToState(Type state)
        {
            m_stateMachine.Reset();
            base.SwitchToState(state);
        }

        public override StateType SwitchToState<StateType>()
        {
            m_stateMachine.Reset();
            return base.SwitchToState<StateType>();
        }

        /// <summary>
        /// Called by the parent's state machine. Iterates this state, and all states below.
        /// </summary>
        public void Tick()
        {
            OnTick();
            m_stateMachine.Tick();
        }

        /// <summary>
        /// Register a parent to this state.
        /// </summary>
        public void AttachParent(CompositeState<T> parent)
        {
            m_parent = parent;
        }
    }
}