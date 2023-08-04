using Joeri.Tools.Structure.StateMachine.Advanced;

public partial class Player
{
    private class Active : CompositeState<Player>
    {
        public override void OnEnter()
        {
            source.m_combat.hitRegister.hurtboxes = source.m_combat.activeMove.hurtboxes;
        }

        public override void OnTick()
        {
            if (source.m_combat.framesExecuting >= source.m_combat.activeMove.recoveryMark)
            {
                Switch(typeof(Recovery));
                return;
            }

            source.m_movement.ApplyIteration();
            source.m_combat.hitRegister.CheckForHits(source.transform.position);
        }
    }
}
