using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move Concept", menuName = "ScriptableObjects/Move Concept", order = 1)]
public partial class MoveConcept : ScriptableObject
{
    [Header("General:")]
    [SerializeField] private Hurtbox[] m_hurtboxes      = null;
    [SerializeField] private AnimationClip m_animation  = null;
    [Space]
    [SerializeField] private MovePossibility[] m_followups = null;

    [Header("Timing:")]
    [SerializeField] private int m_startup  = 10;
    [SerializeField] private int m_active   = 5;
    [SerializeField] private int m_recovery = 10;

    public Hurtbox[] hurtboxes          { get => m_hurtboxes; }
    public AnimationClip animation      { get => m_animation; }
    public MovePossibility[] followups  { get => m_followups; }

    public int startup  { get => m_startup; }
    public int active   { get => m_active; }
    public int recovery { get => m_recovery; }

    public int activeMark   { get => m_startup; }
    public int recoveryMark { get => m_startup + m_active; }
    public int endMark      { get => m_startup + m_active + m_recovery; }
}
