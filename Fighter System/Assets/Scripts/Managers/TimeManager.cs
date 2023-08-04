using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TimeManager
{
    public uint frameCount    = 0;
    public bool paused          = false;

    private int m_hitPauseAmount    = 10;
    private int m_hitPauseTimer     = 0;

    public void Setup()
    {
        GameManager.instance.events.onEntityHit += OnHitPause;
    }

    public void Tick()
    {
        if (paused)
        {
            if (m_hitPauseTimer > m_hitPauseAmount)
            {
                Time.timeScale  = 1f;
                paused          = false;
                m_hitPauseTimer = 0;
            }
            else
            {
                m_hitPauseTimer++;
                return;
            }
        }

        frameCount++;
    }

    public void OnHitPause()
    {
        Time.timeScale  = 0f;
        paused          = true;
    }
}
