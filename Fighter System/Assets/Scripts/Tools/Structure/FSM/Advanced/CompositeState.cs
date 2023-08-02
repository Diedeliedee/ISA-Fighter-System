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
                {
                    Debug.Log($"State: {this} does not have a parent state.");
                    return null;
                }
                return m_parent;
            }
        }
        public CompositeState<T>[] children
        {
            get
            {
                if (m_stateMachine == null || Util.IsUnusableArray(m_stateMachine.states))
                {
                    Debug.Log($"State: {this} does not have any child states.");
                    return null;
                }
                return m_stateMachine.states as CompositeState<T>[];
            }
        }

        protected new CompositeFSM<T> owner { get => base.owner as CompositeFSM<T>; }
        protected T source                  { get; private set; }

        public CompositeState() { }

        public CompositeState(params CompositeState<T>[] children)
        {
            m_stateMachine = new CompositeFSM<T>(children);
            foreach (var child in children) child.AttachParent(this);
        }

        /// <summary>
        /// Called by the parent's state machine. Recursively starts the state composite.
        /// </summary>
        public void Start()
        {
            m_stateMachine?.Start();
        }

        /// <summary>
        /// Called by the parent's state machine. Iterates this state, and all states below.
        /// </summary>
        public void Tick()
        {
            OnTick();
            m_stateMachine?.Tick();
        }

        /// <summary>
        /// Switches to another state based on the passed in parameter, and recursively resets all active child state machines.
        /// </summary>
        public override void SwitchToState(Type state)
        {
            m_stateMachine?.Reset();
            base.SwitchToState(state);
        }

        /// <summary>
        /// Switches to another state based on the generic type, and recursively resets all active child state machines.
        /// </summary>
        public override StateType SwitchToState<StateType>()
        {
            m_stateMachine?.Reset();
            return base.SwitchToState<StateType>();
        }

        /// <summary>
        /// Register a parent to this state.
        /// </summary>
        public void AttachParent(CompositeState<T> parent)
        {
            m_parent = parent;
        }

        /// <summary>
        /// Recursively register the source instance to this state in this, and all child states.
        /// </summary>
        public void RelaySource(T source)
        {
            this.source = source;

            var childStates = children;
            if (childStates == null) return;
            foreach (var child in childStates)
            {
                child.RelaySource(source);
            }
        }
    }
}