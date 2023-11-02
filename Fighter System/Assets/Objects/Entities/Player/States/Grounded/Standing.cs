using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    public class Standing : CompositeState<Player>
    {
        public override void OnEnter()
        {
            source.m_animator.CrossFadeInFixedTime(source.m_idleAnimation.name, 0.066f);
        }

        public override void OnTick()
        {
            if (GameManager.instance.latestInput.joystick.vector.y <= -1)
                Switch(typeof(Crouching));
        }
    }
}
