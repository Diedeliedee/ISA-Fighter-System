using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLogger : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private float m_seperation = 50;

    [Header("Reference:")]
    [SerializeField] private GameObject m_indicatorPrefab;

    private LinkedList<InputIndicator> m_indicators = null;
    private RectTransform m_field                   = null;

    private void Start()
    {
        m_indicators    = new LinkedList<InputIndicator>();
        m_field         = GetComponent<RectTransform>();
    }

    public void AddIndicator(InputPackage package)
    {
        //  Add the indicator to the list.
        m_indicators.AddFirst(Instantiate(m_indicatorPrefab, m_field).GetComponent<InputIndicator>());

        //  Reorganize all the indicators.
        OrganizeIndicators();
    }

    private void OrganizeIndicators()
    {
        var offset = 0f;
        var marked = new List<InputIndicator>();

        foreach (var indicator in m_indicators)
        {
            //  The position of this iteration is the last perceived offset minus seperation value.
            var newPosition = offset - m_seperation;

            //  If the new position would beyond the left side of the screen, mark and continue.
            if (newPosition <= -m_field.sizeDelta.x)
            {
                marked.Add(indicator);
                continue;
            }

            //  If not, reposition the indicator.
            indicator.position  = newPosition;
            offset              = indicator.leftBorder;
        }

        //  Remove all marked indicators from the list.
        foreach (var indicator in marked)
        {
            m_indicators.Remove(indicator);
        }
    }
}
