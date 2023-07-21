using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

namespace Joeri.Tools.Structure.BehaviorTree
{
    public abstract class Node
    {
        private State m_state       = State.Failure;
        private Node m_parent       = null;
        private Node[] m_children   = null;

        public State state { get => m_state; }
        public Node parent
        {
            get
            {
                if (m_parent == null)
                    Debug.LogError($"Node of type {this} does not have a parent node.");
                return m_parent;
            }
        }
        public Node[] children
        {
            get
            {
                if (m_children == null)
                    Debug.LogError($"Node of type {this} does not have any children nodes.");
                return m_parent;
            }
        }

        public Node(params Node[] childrenNodes)
        {
            //  Attach children to the node.
            m_children = childrenNodes;

            //  Attach this node as the childrens' parent.
            foreach (var node in children) node.AttachParent(this);
        }

        /// <summary>
        /// Sets the passed in node as the this node's parent.
        /// </summary>
        public void AttachParent(Node parentNode)
        {
            m_parent = parentNode;
        }

        /// <summary>
        /// Evaluates the node's desired state.
        /// </summary>
        public virtual State Evaluate()
        {
            return RetrieveState(State.Failure);
        }

        /// <returns>The passed in State, while also changing the node's sate property.</returns>
        public State RetrieveState(State stateToReturn)
        {
            m_state = stateToReturn;
            return    stateToReturn;
        }

        public enum State
        {
            Failure = 0,
            Running = 1,
            Succes  = 2,
        }
    }
}
