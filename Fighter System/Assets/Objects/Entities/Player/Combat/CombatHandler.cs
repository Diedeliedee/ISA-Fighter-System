using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

public partial class CombatHandler
{
    //  Dependencies:
    public MoveSet moveset                      = null;
    public HitRegister<PunchingBag> hitRegister = null;
    public Animator animator                    = null;

    //  Cache:
    public MoveConcept activeMove   = null;
    public uint framesExecuting     = 0;

    //  Properties:
    public bool executingMove       { get => activeMove != null; }

    public CombatHandler(MoveSet moveset, HitRegister<PunchingBag> hitRegister, Animator animator)
    {
        hitRegister.Setup(OnHitTarget);

        this.moveset        = moveset;
        this.hitRegister    = hitRegister;
        this.animator       = animator;
    }

    /// <summary>
    /// Executes a move. Any external behavior tree, should react to this source.
    /// </summary>
    public void ExecuteMove(MoveConcept move)
    {
        activeMove      = move;
        framesExecuting = 0;
    }

    /// <returns>True if the input from the game's history corresponds to the recipe of a possible move.</returns>
    public bool CheckForValidInput(out MoveConcept move)
    {
        return CheckForValidInput(GameManager.instance.inputHistory, out move);
    }

    /// <returns>True if the input from the passed in history corresponds to the recipe of a possible move.</returns>
    public bool CheckForValidInput(InputHistory history, out MoveConcept move)
    {
        //  Check for possibilities conssidering the current combat state of the player.
        if (!executingMove) CheckForValidInput(history, out move, moveset.entries);
        else                CheckForValidInput(history, out move, activeMove.followups);

        //  Return true or false accordingly.
        if (move == null)   return false;
                            return true;
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
    private void OnHitTarget(PunchingBag target, Collider2D collider, Hurtbox hurtbox)
    {
        var hitInstance = new HitInstance(activeMove, collider, hurtbox);

        target.OnHit(activeMove.damage, activeMove.stun, activeMove.knockBack);
        GameManager.instance.events.onEntityHit?.Invoke(hitInstance);
    }
}
