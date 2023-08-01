using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class AdvancedFSM<T> : FSM
    {
        public readonly T source;

        public AdvancedFSM(T source)
        {
            this.source = source;
        }

        public CompositeState<T> BuildComposite(params CompositeState<T>[] states)
        {

        }
    } 
}
