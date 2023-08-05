using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TimeManager
{
    public uint frameCount  = 0;
    public bool paused      = false;

    private int m_hitPauseAmount    = 0;
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
                m_hitPauseAmount    = 0;
                m_hitPauseTimer     = 0;

                Time.timeScale  = 1f;
                paused          = false;
            }
            else
            {
                m_hitPauseTimer++;
                return;
            }
        }

        frameCount++;
    }

    public void OnHitPause(int frames)
    {
        m_hitPauseAmount    = frames;
        m_hitPauseTimer     = 0;

        Time.timeScale      = 0f;
        paused              = true;
    }
}
