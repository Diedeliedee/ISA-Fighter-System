using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure.BehaviorTree;
using Joeri.Tools.Structure.StateMachine;

public partial class Player
{
    public class PerformMove : Module<Player>
    {
        //  Dependencies:
        private FSM m_stateMachine = null;

        //  Run-time:
        private float m_velocity        = 0f;
        private bool m_finished         = false;
        private MoveConcept m_followUp  = null;

        //  Properties:
        private CombatHandler combat { get => source.m_combat; }

        public PerformMove(Player source) : base(source) { }

        public override State Evaluate()
        {
            //  If the state machine is null, the task has just started. Create a new one.
            if (m_stateMachine == null)
                m_stateMachine = new FSM(typeof(Startup), new Startup(this), new Active(this), new Recovery(this));

            //  Tick the state machine.
            m_stateMachine.Tick(GameManager.deltaTime);

            //  If the state machine has been placed in a finished state, this node has been successful.
            if (m_finished)
            {
                Cleanup();
                return RetrieveState(State.Succes);
            }

            //  If a follow up move has been assigned.
            if (m_followUp != null)
            {
                var followUp = m_followUp;              //  Save the reference to the follow up.

                Cleanup();                              //  Clean up this node.
                source.m_combat.ExecuteMove(followUp);  //  Execute move.
                return Evaluate();                      //  Call this function again to jump-start the new move in this frame.
            }

            //  Keep iterating.
            source.m_combat.framesExecuting++;
            return RetrieveState(State.Running);
        }

        private void IterateMovement()
        {
            source.transform.position += new Vector3(m_velocity, 0f, 0f);
        }

        private void Cleanup()
        {
            combat.activeMove       = null;
            combat.framesExecuting  = 0;
            m_stateMachine          = null;

            m_velocity = 0f;

            m_finished = false;
            m_followUp = null;
        }

        private class Startup : ModuleState<PerformMove>
        {
            public Startup(PerformMove source) : base(source) { }

            public override void OnEnter()
            {
                if (source.combat.activeMove.movementAmount != 0f)
                {
                    var timeInFrames        = source.combat.activeMove.recoveryMark;
                    var distanceInMeters    = source.combat.activeMove.movementAmount;

                    source.m_velocity = distanceInMeters / timeInFrames;
                }

                //  source.source.m_animator.Play(source.combat.activeMove.animation.name);            
            }

            public override void OnTick(float deltaTime)
            {
                source.IterateMovement();

                if (source.combat.framesExecuting >= source.combat.activeMove.activeMark)
                    SwitchToState(typeof(Active));
            }
        }

        private class Active : ModuleState<PerformMove>
        {
            public Active(PerformMove source) : base(source) { }

            public override void OnEnter()
            {
                source.combat.hitRegister.hurtboxes = source.combat.activeMove.hurtboxes;
            }

            public override void OnTick(float deltaTime)
            {
                if (source.combat.framesExecuting >= source.combat.activeMove.recoveryMark)
                    SwitchToState(typeof(Recovery));

                source.IterateMovement();
                source.combat.hitRegister.CheckForHits();
            }
        }

        private class Recovery : ModuleState<PerformMove>
        {
            public Recovery(PerformMove source) : base(source) { }

            public override void OnEnter()
            {
                source.combat.hitRegister.Clear();
            }

            public override void OnTick(float deltaTime)
            {
                //  If a new input has been found during recovery, assign it in the source.
                if (source.combat.CheckForValidInput(out MoveConcept move))
                {
                    source.m_followUp = move;
                    return;
                }

                //  If the end frame has been reached, tell it to the source.
                if (source.combat.framesExecuting >= source.combat.activeMove.endMark)
                {
                    source.m_finished = true;
                    return;
                }
            }
        }
    }
}
