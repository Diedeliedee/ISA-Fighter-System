using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform m_playerOne;
    [SerializeField] private Transform m_playerTwo;
    [Space]
    [SerializeField] private float m_minimumFOV = 25f;
    [Space]
    [SerializeField] private float m_smoothTime     = 0.1f;
    [SerializeField] private float m_fovMultiplier  = 1f;

    private Camera m_camera = null;

    private float m_posVelocity = 0f;
    private float m_fovVelocity = 0f;

    public void Setup()
    {
        m_camera = GetComponentInChildren<Camera>();

        transform.position      = new Vector3(GetDesiredXPosition(), transform.position.y, transform.position.z);
        m_camera.fieldOfView    = GetDesiredFOV();
    }

    public void Tick()
    {
        var pos = transform.position;
        var fov = m_camera.fieldOfView;

        pos.x   = Mathf.SmoothDamp(pos.x,   GetDesiredXPosition(),  ref m_posVelocity, m_smoothTime, Mathf.Infinity, GameManager.deltaTime);
        fov     = Mathf.SmoothDamp(fov,     GetDesiredFOV(),        ref m_fovVelocity, m_smoothTime, Mathf.Infinity, GameManager.deltaTime);

        transform.position      = pos;
        m_camera.fieldOfView    = fov;
    }

    private float GetDesiredXPosition()
    {
        return (m_playerOne.position.x + m_playerTwo.position.x) / 2;
    }

    private float GetDesiredFOV()
    {
        var adjacent    = Mathf.Abs(transform.position.z);
        var opposite    = Mathf.Abs(m_playerOne.position.x - m_playerTwo.position.x) / 2;
        var desiredFOV  = 0f;

        desiredFOV = Mathf.Atan(opposite / adjacent) * Mathf.Rad2Deg * 2 * m_fovMultiplier / m_camera.aspect;
        desiredFOV = Mathf.Clamp(desiredFOV, m_minimumFOV, desiredFOV);
        return desiredFOV;
    }
}
