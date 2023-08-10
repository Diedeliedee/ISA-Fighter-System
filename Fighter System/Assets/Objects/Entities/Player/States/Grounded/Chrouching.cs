using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeri.Tools;
using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    public class Crouching : CompositeState<Player>
    {
        private Swapper<float> m_speedSwapper = null;

        public override void OnEnter()
        {
            m_speedSwapper          = new Swapper<float>(source.m_crouchSpeed);
            source.m_movement.speed = m_speedSwapper.Swap(source.m_movement.speed);

          //source.m_animator.Play(source.m_crouchAnimation.name);
        }

        public override void OnTick()
        {
            if (GameManager.instance.latestInput.joystick.vector.y > -1)
                Switch(typeof(Standing));
        }

        public override void OnExit()
        {
            source.m_movement.speed = m_speedSwapper.Swap(source.m_movement.speed);
            m_speedSwapper          = null;
        }
    }
}
