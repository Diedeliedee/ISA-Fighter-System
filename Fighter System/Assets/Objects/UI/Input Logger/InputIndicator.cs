using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputIndicator : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private float m_inactiveMulitplier = 0.5f;

    [Header("Reference:")]
    [SerializeField] private RectTransform m_arrow; 
    [SerializeField] private Image m_punchButton; 
    [SerializeField] private Image m_kickButton;

    private RectTransform m_transform = null;

    public float position
    {
        get => m_transform.anchoredPosition.x;
        set
        {
            var xPos = value;
            var yPos = 0f;

            m_transform.anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    public float leftBorder
    {
        get => m_transform.anchoredPosition.x - m_transform.sizeDelta.x;
    }

    public void Configure(InputPackage input)
    {
        //  Get transform reference.
        m_transform = GetComponent<RectTransform>();

        //  Initiate values.
        var arrowAngle  = 0f;
        var punchColor  = m_punchButton.color;
        var kickColor   = m_kickButton.color;

        //  Alter values.
        arrowAngle                                  = Vector2.SignedAngle(Vector2.up, input.joystick.vector);
        if (!input.punchButton.holding) punchColor  *= m_inactiveMulitplier;
        if (!input.kickButton.holding) kickColor    *= m_inactiveMulitplier;

        //  Reassign values.
        m_arrow.localRotation   = Quaternion.Euler(0f, 0f, arrowAngle);
        m_punchButton.color     = punchColor;
        m_kickButton.color      = kickColor;
    }
}
