using UnityEngine;

[System.Serializable]
public class MovePossibility
{
    [SerializeField] private MoveRecipe     m_recipe;
    [SerializeField] private MoveConcept    m_move;

    public MoveRecipe recipe    { get => m_recipe; }
    public MoveConcept move     { get => m_move; }

    /// <returns>True if this move possibility's recipe corresponds to the passed in history.</returns>
    public bool CheckForValidInput(InputHistory history, out MoveConcept concept)
    {
        concept = m_move;
        return m_recipe.ConfirmRecipe(history);
    }
}
