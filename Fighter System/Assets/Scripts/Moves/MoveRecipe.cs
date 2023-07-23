using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class MoveRecipe
{
    [SerializeField] private InputRequirement[] m_precendences  = new InputRequirement[1];
    [SerializeField] private InputRequirement m_activator       = null;

    /// <returns>True if this recipe corresponds to the player's input.</returns>
    public bool ConfirmRecipe(InputHistory history)
    {
        var capture     = history.lastCapture;
        var requirement = m_activator;

        //  If the activator isn't valid, don't bother checking preceding inputs.
        if (!ConfirmInput(capture.package, requirement))
        {
            return false;
        }

        //  Backtrack the precedences, if there are any.
        for (int i = m_precendences.Length - 1; i >= 0; i--)
        {
            capture     = capture.previous;
            requirement = m_precendences[i];

            //  First check if the preceding package, and preceding recipe align,
            if (!ConfirmInput(capture.package, requirement)) return false;

            //  Then check if the two inputs are in proper distance from each other.
            if (capture.distanceToNext > requirement.leeway) return false;
        }

        //  If no false has been returned, the input history is corresponding to the recipe.
        return true;
    }

    /// <returns>True if the package corresponds to the requirement.</returns>
    public bool ConfirmInput(InputPackage package, InputRequirement requirement)
    {
        var directionOkay   = true;
        var punchOkay       = true;
        var kickOkay        = true;

        if (requirement.directionMatters)   directionOkay   = requirement.desiredDirection  == package.joystick.direction;
                                            punchOkay       = requirement.shouldPressPunch  == package.punchButton.holding;
                                            kickOkay        = requirement.shouldPressKick   == package.kickButton.holding;

        return directionOkay && punchOkay && kickOkay;
    }

    [System.Serializable]
    public class InputRequirement
    {
        [Header("Move Requirements:")]
        public bool                     directionMatters;
        public JoystickInput.Direction  desiredDirection;
        [Space]
        public bool                     shouldPressPunch;
        public bool                     shouldPressKick;

        [Header("Meta:")]
        [Min(0)] public int             leeway;
    }
}
