using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    private class Startup : CompositeState<Player>
    {
        public override void OnEnter()
        {
            if (source.m_combat.activeMove.movementAmount != 0f)
            {
                var timeInFrames        = source.m_combat.activeMove.recoveryMark;
                var distanceInMeters    = source.m_combat.activeMove.movementAmount;

                source.m_movement.horizontalVelocity = distanceInMeters / timeInFrames;
            }

            if (source.m_combat.activeMove.animation != null)
                source.m_animator.Play(source.m_combat.activeMove.animation.name);
        }

        public override void OnTick()
        {
            source.m_movement.ApplyIteration();

            if (source.m_combat.framesExecuting >= source.m_combat.activeMove.activeMark)
                SwitchToState(typeof(Active));
        }
    }
}
