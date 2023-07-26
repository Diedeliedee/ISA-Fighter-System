using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

public class CombatHandler
{
    //  Dependencies:
    private HitRegister<PunchingBag> m_hitRegister  = null;
    private Animator m_animator                     = null;

    //  Cache:
    private MoveSet m_moveset           = null;
    private MoveConcept m_activeMove    = null;

    //  Properties:
    public bool executingMove       { get => m_activeMove != null; }
    public MoveConcept activeMove   { get => m_activeMove; }

    public CombatHandler(MoveSet moveSet, HitRegister<PunchingBag> hitRegister, Animator animator)
    {
        hitRegister.Setup(OnHitTarget);

        m_moveset = moveSet;

        m_hitRegister   = hitRegister;
        m_animator      = animator;
    }

    /// <summary>
    /// Searches for any possible input to execute a move in the given situation.
    /// Will do nothin if no possibility has been found.
    /// </summary>
    /// <returns>True if a move is executed.</returns>
    public bool ExecuteMoveUponValidInput(InputHistory history)
    {
        MoveConcept moveToExecute;

        //  If the player is not actively executing a move, search in the move entries, in the moveset.
        if (!executingMove) CheckForValidInput(history, out moveToExecute, m_moveset.entries);

        //  If the player is performing a move, search for any possible follow-ups ot the move.
        else                CheckForValidInput(history, out moveToExecute, m_activeMove.followups);

        //  If the move to execute is not found, return false.
        if (moveToExecute == null) return false;

        //  Otherwise, execute the move.
        ExecuteMove(moveToExecute);
        return true;
    }

    /// <summary>
    /// Executes a move. Any external behavior tree, should react to this source.
    /// </summary>
    public void ExecuteMove(MoveConcept move)
    {
        m_activeMove = move;

        //  Debug: Enable this line when animations are worked out.
        //m_animator.Play(move.animation.name);
    }
    
    /// <summary>
    /// Called by an external behavior tree, should only be called while the move is in active state.
    /// No startup, no cooldown.
    /// </summary>
    public void TickMove()
    {
        m_hitRegister.CheckForHits();
    }

    /// <summary>
    /// Finished the move, preferably called by animator event.
    /// Any external behavior tree should react to this source.
    /// </summary>
    public void FinishMove()
    {
        if (m_activeMove == null)
        {
            Debug.LogWarning("FinishMove() is being called when a move isn't active, check it out.");
            return;
        }

        m_hitRegister.Clear();
        m_activeMove = null;
    }

    /// <summary>
    /// Searches the given possibility array's recipe's for any corresponding to the given history.
    /// </summary>
    /// <returns>True if any of the recipe's correspond to the given history.</returns>
    public bool CheckForValidInput(InputHistory history, out MoveConcept move, params MovePossibility[] possibilities)
    {
        //  Loop through the possibilities.
        foreach (var possibility in possibilities)
        {
            //  If the history does not correspond with the possibility's recipe, move to the next.
            if (!possibility.CheckForValidInput(history, out move)) continue;

            //  If it does, a move can be executed.
            return true;
        }

        //  Return false if the array is unusable.
        move = null;
        return false;
    }

    /// <summary>
    /// Called by an event, executes proper logic once a target has been hit.
    /// </summary>
    private void OnHitTarget(PunchingBag target)
    {
        Debug.Log("Target hit!!!!");
    }
}
