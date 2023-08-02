using Joeri.Tools.Structure.StateMachine.Advanced;

    public partial class Player
    {
        private class Recovery : CompositeState<Player>
        {
            public override void OnEnter()
            {
                source.m_combat.hitRegister.Clear();
            }

            public override void OnTick()
            {
                //  If a new input has been found during recovery, assign it in the source.
                if (source.m_combat.CheckForValidInput(out MoveConcept move))
                {
                    source.m_combat.ExecuteMove(move);
                    SwitchToState(typeof(Startup));
                }
            }
        }
}
