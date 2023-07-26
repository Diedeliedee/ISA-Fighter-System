﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

[System.Serializable]
public class HitRegister<T> where T : Object
{
    [SerializeField] private List<Hurtbox> m_hurtboxes;
    [SerializeField] private LayerMask m_hitmask;

    private HashSet<Collider2D> m_caughtColliders   = null;
    private System.Action<T> m_onHit                = null;

    public void Setup()
    {
        m_hurtboxes         = new List<Hurtbox>();
        m_caughtColliders   = new HashSet<Collider2D>();
    }

    /// <summary>
    /// Checks whether the hurtboxes caught anything.
    /// </summary>
    public void CheckForHits()
    {
        if (Util.IsUnusableList(m_hurtboxes)) return;                               //  Return if there are no hurtboxes.
        foreach (var hurtbox in m_hurtboxes)                                        //  Otherwise, loop through every hurtbox.
        {
            if (!hurtbox.Hit(m_hitmask, out Collider2D[] hitColliders)) continue;   //  Skip iteration if no colliders have been hit.
            foreach (var collider in hitColliders)                                  //  If colliders have been hit, loop through them.
            {
                if (m_caughtColliders.Contains(collider)) continue;                 //  Skip iteration if collider has already been caught.
                m_caughtColliders.Add(collider);                                    //  Save collider if it hasn't been caught yet.
                if (!collider.TryGetComponent(out hitObject)) continue;             //  Skip iteration if the caught collider doesn't have what we're looking for.
                return true;                                                        //  If we do find what we're looking for, return true.
            }
        }
        return false;                                                               //  Return false if none of the hurtboxes in the loop succesfully hit something.
    }


    /// <summary>
    /// Clears all caught colliders from the list.
    /// </summary>
    public void Clear()
    {
        m_caughtColliders.Clear();
    }

    /// <summary>
    /// Draws the hurtboxes, if there are any active.
    /// </summary>
    public void Draw(float zPos)
    {
        if (Util.IsUnusableList(m_hurtboxes))   return;
        foreach (var hurtbox in m_hurtboxes)    hurtbox.Draw(zPos);
    }
}
