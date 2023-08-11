using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Joeri.Tools.Utilities;

public class InputIndicator : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private float m_inactiveMulitplier = 0.5f;

    [Header("Reference:")]
    [SerializeField] private RectTransform m_arrow;
    [Space]
    [SerializeField] private Image m_arrowImage;
    [SerializeField] private Image m_punchImage;
    [SerializeField] private Image m_kickImage;

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

        //  Arrow.
        if (input.joystick.direction != JoystickInput.Direction.None)
        {
            //  Change sprite to the arrow.
            m_arrowImage.gameObject.SetActive(true);

            //  Rotate the arrow to the desired angle.
            m_arrow.localRotation = Quaternion.Euler(0f, 0f, -Vectors.VectorToAngle(input.joystick.vector));

            //  Rotate around it's y axis to get the proper shine. :)
            if (input.joystick.vector.x > 0 || input.joystick.vector.y < 0) 
                m_arrow.Rotate(0f, 180f, 0f);
        }

        //  Button.
        var punchColor  = m_punchImage.color;
        var kickColor   = m_kickImage.color;

        if (!input.punchButton.holding) punchColor  *= m_inactiveMulitplier;
        if (!input.kickButton.holding) kickColor    *= m_inactiveMulitplier;

        m_punchImage.color  = punchColor;
        m_kickImage.color   = kickColor;
    }
}
