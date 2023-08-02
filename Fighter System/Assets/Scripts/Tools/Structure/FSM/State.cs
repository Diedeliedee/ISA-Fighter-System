using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joeri.Tools.Structure.StateMachine
{
    public abstract class State : IState
    {
        /// <summary>
        /// The state machine this state is a part of.
        /// </summary>
        protected FSM owner { get; private set; }

        public virtual void OnEnter()   { }

        public virtual void OnTick()    { }

        public virtual void OnExit()    { }

        public virtual void SwitchToState(System.Type state)
        {
            owner.SwitchToState(state);
        }

        /// <summary>
        /// Called whenever the finite state machine the state is in, is created.
        /// </summary>
        public virtual void Setup(FSM owner)
        {
            this.owner = owner;
        }


        /// <summary>
        /// Requests the state machine to switch to another state based on the passed in generic type.
        /// </summary>
        public virtual T SwitchToState<T>() where T : State
        {
            return owner.SwitchToState<T>();
        }

        /// <summary>
        /// Abstract function allowing for gizmos to be drawn by the state machine.
        /// </summary>
        public virtual void OnDrawGizmos() { }
    }
}
