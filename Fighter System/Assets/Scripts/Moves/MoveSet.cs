using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move Set", menuName = "ScriptableObjects/Move Set", order = 0)]
public partial class MoveSet : ScriptableObject
{
    [SerializeField] private MovePossibility[] m_entries = new MovePossibility[1];

    public MovePossibility[] entries { get => m_entries; }
}
