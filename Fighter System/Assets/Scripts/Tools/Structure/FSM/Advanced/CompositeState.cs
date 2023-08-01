using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public abstract class CompositeState<T> : State
    {
        private CompositeState<T> m_parent      = null;
        private AdvancedFSM<T> m_stateMachine   = null;

        public CompositeState<T> parent
        {
            get
            {
                if (m_parent == null)
                    Debug.LogError($"State: {this} does not have a parent state.");
                return m_parent;
            }
        }

        protected new AdvancedFSM<T> owner  { get => base.owner as AdvancedFSM<T>; }
        protected T source                  { get => owner.source; }

        public void AttachParent(CompositeState<T> parent)
        {
            m_parent = parent;
        }


    }
}