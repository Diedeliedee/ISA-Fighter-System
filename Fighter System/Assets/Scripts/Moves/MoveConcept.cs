using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move Concept", menuName = "ScriptableObjects/Move Concept", order = 1)]
public partial class MoveConcept : ScriptableObject
{
    [SerializeField] private Hurtbox[] m_hurtboxes          = null;
    [SerializeField] private AnimationClip m_animation      = null;
    [Space]
    [SerializeField] private MovePossibility[] m_followups  = null;

    public Hurtbox[] hurtboxes          { get => m_hurtboxes; }
    public AnimationClip animation      { get => m_animation; }
    public  MovePossibility[] followups { get => m_followups; }
}
