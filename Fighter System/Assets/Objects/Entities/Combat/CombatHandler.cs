﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

public class CombatHandler
{
    //  Dependencies:
    private Animator m_animator = null;

    //  Cache:
    private MoveSet m_moveset           = null;
    private MoveConcept m_activeMove    = null;

    //  Properties:
    public bool executingMove       { get => m_activeMove != null; }
    public MoveConcept activeMove   { get => m_activeMove; }

    public CombatHandler(MoveSet moveSet, Animator animator)
    {
        m_moveset   = moveSet;
        m_animator  = animator;
    }

    /// <summary>
    /// Searches for any possible input to execute a move in the given situation.
    /// Will do nothin if no possibility has been found.
    /// </summary>
    public void ExecuteMoveUponValidInput(InputHistory history)
    {
        MoveConcept moveToExecute;

        //  If the player is not actively executing a move, search in the move entries, in the moveset.
        if (!executingMove) CheckForValidInput(history, out moveToExecute, m_moveset.entries);

        //  If the player is performing a move, search for any possible follow-ups ot the move.
        else                CheckForValidInput(history, out moveToExecute, m_activeMove.followups);

        if (moveToExecute != null) ExecuteMove(moveToExecute);
    }

    /// <summary>
    /// Executes a move. Any external behavior tree, should react to this source.
    /// </summary>
    public void ExecuteMove(MoveConcept move)
    {
        m_activeMove = move;

        m_animator.Play(move.animation.name);
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
}
