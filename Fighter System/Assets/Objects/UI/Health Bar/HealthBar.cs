using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private float m_smoothTime = 0.05f;

    [Header("Reference:")]
    [SerializeField] private RectTransform m_bar;

    private float m_defaultWidth    = 0f;

    private float m_desiredWidth    = 0f;
    private float m_barVelocity     = 0f;

    public void Setup()
    {
        m_defaultWidth                                  = m_bar.sizeDelta.x;
        m_desiredWidth                                  = m_defaultWidth;

        GameManager.instance.events.onBagHealthChange   += OnHealthChange;
    }

    public void Tick()
    {
        var desiredWidth = m_bar.sizeDelta.x;

        desiredWidth    = Mathf.SmoothDamp(desiredWidth, m_desiredWidth, ref m_barVelocity, m_smoothTime, Mathf.Infinity, GameManager.deltaTime);
        m_bar.sizeDelta = new Vector2(desiredWidth, m_bar.sizeDelta.y);
    }

    public void OnHealthChange(float percentage)
    {
        m_desiredWidth = percentage * m_defaultWidth;
    }
}
