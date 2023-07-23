using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move Concept", menuName = "ScriptableObjects/Move Concept", order = 1)]
public partial class MoveConcept : ScriptableObject
{
    [SerializeField] private AnimationClip m_animation      = null;
    [SerializeField] private MovePossibility[] m_followups  = null;

    public AnimationClip animation      { get => m_animation; }
    public  MovePossibility[] followups { get => m_followups; }
}
