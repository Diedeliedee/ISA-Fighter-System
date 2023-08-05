﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

namespace Joeri.Tools
{
    public class ShakeInstancer
    {
        private float m_magnitude       = 0f;
        private float m_frequency       = 0f;
        private float m_time              = 0f;  
        private Timer m_tickTimer       = null;
        private float m_velocity        = 0f;

        private Vector3 m_startPosition = Vector3.zero;
        private Vector3 m_currentOffset = Vector3.zero;

        private const float m_epsilon   = 0.05f;

        public Vector3 startPosition    { get => m_startPosition; }
        public Vector3 currentOffset    { get => m_currentOffset; }
        public Vector3 currentPosition  { get => m_startPosition + m_currentOffset; }

        public float magnitude
        {
            get => m_magnitude;
            set
            {
                m_magnitude = value;
                ReInitialize();
            }
        }

        public float frequency
        {
            get => m_frequency;
            set
            {
                m_frequency = value;
                ReInitialize();
            }
        }

        public float time
        {
            get => m_time;
            set
            {
                m_time = value;
                ReInitialize();
            }
        }

        public float mark { get => m_tickTimer.time; }

        public ShakeInstancer(Vector3 startPosition, float magnitude, float frequency, float time)
        {
            m_startPosition = startPosition;
            m_magnitude     = magnitude;
            m_frequency     = frequency;
            m_time          = time;
            m_tickTimer     = new Timer(1f / frequency);

            m_tickTimer.timer = m_tickTimer.time;
        }

        /// <summary>
        /// Iterates the shake instance forward.
        /// </summary>
        /// <returns>The position generated by the created offset.</returns>
        public Vector3 GetPosition(float deltaTime)
        {
            return m_startPosition + GetOffset(deltaTime);
        }

        /// <summary>
        /// Iterates the shake instance forward.
        /// </summary>
        /// <returns>The offset generated.</returns>
        public Vector3 GetOffset(float deltaTime)
        {
            //  Rest if the magnitude is lower than epsilon.
            if (m_magnitude <= m_epsilon)
            {
                m_currentOffset = Vector3.zero;
                return Vector3.zero;
            }
            
            m_magnitude = Mathf.SmoothDamp(m_magnitude, 0f, ref m_velocity, m_time, Mathf.Infinity, deltaTime); //  Gradually decrease the magnitude.

            if (m_frequency < Mathf.Infinity && !m_tickTimer.ResetOnReach(deltaTime)) return m_currentOffset;   //  Wait until the timer has reached it's end.
            
            m_currentOffset = Vectors.RandomSpherePoint(m_magnitude);                                           //  Create new offset.
            return m_currentOffset;                                                                             //  Return the offset.
        }

        public void ReInitialize()
        {
            m_velocity      = 0f;
            m_tickTimer     = new Timer(1f / m_frequency);
            m_currentOffset = Vector3.zero;
        }
    }
}
